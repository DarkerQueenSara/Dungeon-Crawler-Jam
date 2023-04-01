using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    /// <summary>
    /// The class that manages the game's HUD.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class PlayerHUD : MonoBehaviour
    {
        #region SingleTon

        /// <summary>
        /// Gets the sole instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static PlayerHUD Instance { get; private set; }

        /// <summary>
        /// Awakes this instance (if none exist).
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
        /// The energy text
        /// </summary>
        public TextMeshProUGUI energyText;
        /// <summary>
        /// The gold text
        /// </summary>
        public TextMeshProUGUI goldText;
        
        /// <summary>
        /// The log messages
        /// </summary>
        public List<TextMeshProUGUI> logMessages;
        /// <summary>
        /// The filled slots in the message log
        /// </summary>
        private bool[] _filledSlots;
        /// <summary>
        /// The log queue
        /// </summary>
        private Queue<string> _logQueue;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            //In order to show only a certain number of text messages on the screen, we use a Queue, since it's FIFO.
            _logQueue = new Queue<string>();

            foreach (var text in logMessages) text.text = "";

            _filledSlots = new bool[logMessages.Count];
            for (var i = 0; i < _filledSlots.Length; i++) _filledSlots[i] = false;

            AddMessage("Hello and welcome, adventurer, to yet another dungeon!");
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update()
        {
            //Each frame we update how much energy/traps/gold the player has,
            //as well as how many turns are left before they take damage from sleep loss.
            energyText.text = PlayerEntity.Instance.health.currentHealth + "/" + PlayerEntity.Instance.health.maxHealth;
            goldText.text = PlayerEntity.Instance.inventory.CurrentGold.ToString();
        }

        /// <summary>
        /// Adds a message to the log.
        /// </summary>
        /// <param name="newMessage">The new message.</param>
        public void AddMessage(string newMessage)
        {
            _logQueue.Enqueue("Turn " + TurnManager.Instance.CurrentTurn + ": " + newMessage);
            //If the number of messages as reached its maximum, we remove the oldest from the queue.
            if (_logQueue.ToArray().Length > logMessages.Count) _logQueue.Dequeue();
            var messages = _logQueue.ToArray();
            for (var i = 0; i < messages.Length; i++) logMessages[i].text = messages[i];
        }
    }
}