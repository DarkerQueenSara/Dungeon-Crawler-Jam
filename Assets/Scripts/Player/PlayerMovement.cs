using System.Collections;
using Managers;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Manages the player's movement
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// The layers of the obstacles
        /// </summary>
        public LayerMask obstacles;
        /// <summary>
        /// The original and target positions
        /// </summary>
        private Vector3 _origPos, _targetPos;
        /// <summary>
        /// The original and target rotations
        /// </summary>
        private Vector3 _origRot, _targetRot;
        /// <summary>
        /// Gets a value indicating whether this instance is moving.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is moving; otherwise, <c>false</c>.
        /// </value>
        public bool IsMoving { get; private set; }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update()
        {
            // If the player can move, we check for a WASD input
            if (!TurnManager.Instance.CanMove()) return;
            if (Input.GetKeyDown(KeyCode.W))
                StartCoroutine(MovePlayer(transform.forward));
            if (Input.GetKeyDown(KeyCode.A))
                StartCoroutine(MovePlayer(transform.right * -1));
            if (Input.GetKeyDown(KeyCode.S))
                StartCoroutine(MovePlayer(transform.forward * -1));
            if (Input.GetKeyDown(KeyCode.D))
                StartCoroutine(MovePlayer(transform.right));
            if (Input.GetKeyDown(KeyCode.Q))
                StartCoroutine(RotatePlayer(-90));
            if (Input.GetKeyDown(KeyCode.E))
                StartCoroutine(RotatePlayer(90));
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public IEnumerator MovePlayer(Vector3 direction)
        {
            IsMoving = true;

            var elapsedTime = 0.0f;

            _origPos = transform.position;
            _targetPos = _origPos + direction;

            //If there's an obstacle we stop
            Collider[] col =
                Physics.OverlapBox(_targetPos, new Vector3(0.25f, 3f, 0.25f), Quaternion.identity, obstacles);
            if (col.Length > 0)
            {
                IsMoving = false;

                yield break;
            }

            //We begin processing the turn prior to beginning the movement, so the gremlins move at the same time,
            //not after the player moves
            TurnManager.Instance.ProcessTurn(_targetPos);

            //PlayerEntity.Instance.audioManager.Play("Moving");

            //Using a coroutine, we move the player without teleporting them.
            while (elapsedTime < TurnManager.Instance.unitTimeToMove)
            {
                transform.position = Vector3.Lerp(_origPos, _targetPos, elapsedTime / TurnManager.Instance.unitTimeToMove);
                elapsedTime += Time.deltaTime;

                yield return null;
            }


            transform.position = _targetPos;
            IsMoving = false;
        }
        
        /// <summary>
        /// Rotates the player.
        /// </summary>
        /// <param name="angle">The angle increment.</param>
        /// <returns></returns>
        public IEnumerator RotatePlayer(int angle)
        {
            IsMoving = true;

            _origRot = transform.rotation.eulerAngles;
            _targetRot = _origRot + Vector3.up * angle;

            var elapsedTime = 0.0f;
            
            //We begin processing the turn prior to beginning the movement, so the enemies move at the same time,
            //not after the player moves
            TurnManager.Instance.ProcessTurn(transform.position);

            //PlayerEntity.Instance.audioManager.Play("Moving");

            //Using a coroutine, we move the player without teleporting them.
            while (elapsedTime < TurnManager.Instance.unitTimeToMove)
            {
                transform.rotation = Quaternion.Euler(Vector3.Lerp(_origRot, _targetRot, elapsedTime / TurnManager.Instance.unitTimeToMove));
                elapsedTime += Time.deltaTime;

                yield return null;
            }
            
            transform.rotation = Quaternion.Euler(_targetRot);
            IsMoving = false;
        }
    }
}