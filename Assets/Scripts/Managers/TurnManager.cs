using System.Collections;
using System.Collections.Generic;
using Audio;
using Enemies;
using Items;
using Player;
using Puzzles;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// The TurnManager processes each turn of the game
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class TurnManager : MonoBehaviour
    {
        #region SingleTon

        /// <summary>
        /// Gets the sole instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static TurnManager Instance { get; private set; }

        /// <summary>
        /// Awakes this instance (if none have been created already).
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        #endregion

        /// <summary>
        /// The time it takes for a unit to move to a new position
        /// </summary>
        public float unitTimeToMove = 0.2f;

        /// <summary>
        /// The portal in map
        /// </summary>
        [HideInInspector] public Portal portalInMap;
        
        /// <summary>
        /// The AudioManager
        /// </summary>
        [HideInInspector] public AudioManager audioManager;

        /// <summary>
        /// The enemies in map
        /// </summary>
        public List<StateMachine> _enemiesInMap;
        
        /// <summary>
        /// The player start position
        /// </summary>
        private Vector3 _playerStartPosition;
        
        /// <summary>
        /// Gets a value indicating whether [processing turn].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [processing turn]; otherwise, <c>false</c>.
        /// </value>
        public bool ProcessingTurn { get; private set; }

        /// <summary>
        /// Gets the current turn.
        /// </summary>
        /// <value>
        /// The current turn.
        /// </value>
        public int CurrentTurn { get; private set; }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            //Create the lists if they haven't been created yet 
            _enemiesInMap ??= new List<StateMachine>();
            audioManager = GetComponent<AudioManager>();
            //Start ambience sound in World 1
            audioManager.Play("world01-default-ambience");
        }

        /// <summary>
        /// Adds a zombie to the manager.
        /// </summary>
        /// <param name="g">The zombie.</param>
        public void AddZombie(StateMachine g)
        {
            _enemiesInMap ??= new List<StateMachine>();
            _enemiesInMap.Add(g);
        }
        
        /// <summary>
        /// Gets the current number of zombies.
        /// </summary>
        /// <returns>Number of zombies</returns>
        public int GetNumberOfZombies()
        {
            return _enemiesInMap.Count;
        }

        /// <summary>
        /// Processes the turn.
        /// </summary>
        /// <param name="playerPos">The player's position.</param>
        public void ProcessTurn(Vector3 playerPos)
        {
            if (ProcessingTurn) return;
            ProcessingTurn = true;
            StartCoroutine(TurnCoroutine(playerPos));
        }

        /// <summary>
        /// A coroutine that performs everything required to conclude a turn
        /// </summary>
        /// <param name="playerPos">The player's position.</param>
        /// <returns></returns>
        private IEnumerator TurnCoroutine(Vector3 playerPos)
        {
            //Move all the zombies
            foreach (var z in _enemiesInMap) z.RunStateMachine(playerPos);

            //Wait until they have finished moving
            yield return new WaitForSeconds(unitTimeToMove);
            
            //Increase the number of turns, and if the right amount has passed, take damage from lack of sleep
            CurrentTurn++;

            ProcessingTurn = false;
        }

        /// <summary>
        /// Determines whether this instance can move.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can move; otherwise, <c>false</c>.
        /// </returns>
        public bool CanMove()
        {
            return !ProcessingTurn && !EntitiesAreMoving();
        }

        /// <summary>
        /// Check if the player or zombies are moving
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the player or a zombie is still moving; otherwise, <c>false</c>.
        /// </returns>
        private bool EntitiesAreMoving()
        {
            foreach (var z in _enemiesInMap)
                if (z.currentState.isActing)
                    return true;

            return PlayerEntity.Instance.movement.IsMoving;
        }
    }
}