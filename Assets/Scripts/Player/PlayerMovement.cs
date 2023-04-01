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
                StartCoroutine(MovePlayer(Vector3.up));
            if (Input.GetKeyDown(KeyCode.A))
                StartCoroutine(MovePlayer(Vector3.left));
            if (Input.GetKeyDown(KeyCode.S))
                StartCoroutine(MovePlayer(Vector3.down));
            if (Input.GetKeyDown(KeyCode.D))
                StartCoroutine(MovePlayer(Vector3.right));
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        private IEnumerator MovePlayer(Vector3 direction)
        {
            IsMoving = true;

            var elapsedTime = 0.0f;

            _origPos = transform.position;
            _targetPos = _origPos + direction;

            //If there's an obstacle we stop
            if (Physics2D.OverlapBox(_targetPos, new Vector2(0.5f, 0.5f), 0, obstacles) != null)
            {
                IsMoving = false;

                yield break;
            }

            //We begin processing the turn prior to beginning the movement, so the gremlins move at the same time,
            //not after the player moves
            TurnManager.Instance.ProcessTurn(_targetPos);

            PlayerEntity.Instance.audioManager.Play("Moving");

            //Using a coroutine, we move the player without teleporting them.
            while (elapsedTime < TurnManager.Instance.unitTimeToMove)
            {
                transform.position =
                    Vector3.Lerp(_origPos, _targetPos, elapsedTime / TurnManager.Instance.unitTimeToMove);
                elapsedTime += Time.deltaTime;

                yield return null;
            }


            transform.position = _targetPos;
            IsMoving = false;
        }
    }
}