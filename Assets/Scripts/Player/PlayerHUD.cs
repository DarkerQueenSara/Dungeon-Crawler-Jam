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
        /// The health bar image
        /// </summary>
        public Image healthBar;

        /// <summary>
        /// The buttons in the UI to move the player
        /// </summary>
        public Button forwardButton, backwardButton, leftButton, rightButton, rotateLeftButton, rotateRightButton;

        /// <summary>
        /// The game object featuring the item box UI
        /// </summary>
        public GameObject itemBoxUI;
        
        /// <summary>
        /// The button to close the item box
        /// </summary>
        public Button closeBoxButton;
        
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
            
            forwardButton.onClick.AddListener(() => MovePlayer(PlayerEntity.Instance.gameObject.transform.forward));
            backwardButton.onClick.AddListener(() => MovePlayer(PlayerEntity.Instance.gameObject.transform.forward * -1));
            leftButton.onClick.AddListener(() => MovePlayer(PlayerEntity.Instance.gameObject.transform.right * -1));
            rightButton.onClick.AddListener(() => MovePlayer(PlayerEntity.Instance.gameObject.transform.right));
            rotateLeftButton.onClick.AddListener(() => RotatePlayer(-90));
            rotateRightButton.onClick.AddListener(() => RotatePlayer(90));
            closeBoxButton.onClick.AddListener(CloseItemBox);
        }

        private static void MovePlayer(Vector3 direction)
        {
            if (!TurnManager.Instance.CanMove()) return;
            PlayerEntity.Instance.movement.StartCoroutine(PlayerEntity.Instance.movement.MovePlayer(direction));
        }
        
        private static void RotatePlayer(int angle)
        {
            if (!TurnManager.Instance.CanMove()) return;
            PlayerEntity.Instance.movement.StartCoroutine(PlayerEntity.Instance.movement.RotatePlayer(angle));
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update()
        {
            healthBar.fillAmount = 1.0f * PlayerEntity.Instance.health.currentHealth / PlayerEntity.Instance.health.maxHealth;
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

        public void OpenItemBox()
        {
            itemBoxUI.SetActive(true);
        }

        private void CloseItemBox()
        {
            itemBoxUI.SetActive(false);
        }
    }
}