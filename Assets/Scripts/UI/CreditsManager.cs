using Audio;
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
        public Button replayButton;
        /// <summary>
        /// The exit game button
        /// </summary>
        public Button exitButton;
        /// <summary>
        /// The AudioManager
        /// </summary>
        private AudioManager _audioManager;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            replayButton.onClick.AddListener(ReplayGame);
            exitButton.onClick.AddListener(ExitGame);
            _audioManager = GetComponent<AudioManager>();
            _audioManager.Play("MenuMusic");
        }

        /// <summary>
        /// Replays the game.
        /// </summary>
        private static void ReplayGame()
        {
            SceneManager.LoadScene(1);
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