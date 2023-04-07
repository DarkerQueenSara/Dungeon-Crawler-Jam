using System.Collections;
using Audio;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Application = UnityEngine.Device.Application;

namespace UI
{
    /// <summary>
    /// The class that manages the credits/end screen
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class CreditsManager : MonoBehaviour
    {
        /// <summary>
        /// The replay game button
        /// </summary>
        public Button backToTitle;
        /// <summary>
        /// The exit game button
        /// </summary>
        public Button exitButton;
        /// <summary>
        /// The AudioManager
        /// </summary>
        private AudioManager _audioManager;

        public Transform creditsTransform;
        public float creditsDuration;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            backToTitle.onClick.AddListener(ReplayGame);
            exitButton.onClick.AddListener(ExitGame);
            backToTitle.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(false);
            _audioManager = GetComponent<AudioManager>();
            _audioManager.Play("MenuMusic");
        }


        /// <summary>
        /// Replays the game.
        /// </summary>
        private static void ReplayGame()
        {
            GameManager.Instance.LoadTitleScreen();
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        private static void ExitGame()
        {
            Application.Quit();
        }
    }
}