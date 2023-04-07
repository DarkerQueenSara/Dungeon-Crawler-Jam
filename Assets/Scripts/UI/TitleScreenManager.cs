using System.Collections;
using System.IO;
using Audio;
using Managers;
using Managers.Save_System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// This class manages the title screen.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class TitleScreenManager : MonoBehaviour
    {
        /// <summary>
        /// The start game button
        /// </summary>
        [Header("Buttons")] public Button startButton;
        public Button loadButton;
        /// <summary>
        /// The tutorial button
        /// </summary>
        public Button tutorialButton;
        /// <summary>
        /// The back button
        /// </summary>
        public Button backButton;
        /// <summary>
        /// The exit button
        /// </summary>
        public Button exitButton;

        /// <summary>
        /// The title screen
        /// </summary>
        [Header("Screens")] public GameObject titleScreen;

        /// <summary>
        /// The tutorial screen
        /// </summary>
        public GameObject tutorialScreen;

        /// <summary>
        /// The audio manager
        /// </summary>
        private AudioManager _audioManager;

        [Header("Fancy Animation Stuff")] public float fadeDuration;
        public TextMeshProUGUI gameTitle;
        
        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            startButton.onClick.AddListener(StartGame);
            if (File.Exists(GameManager.Instance.savePath)){
                loadButton.onClick.AddListener(LoadGame);
            } else
            {
                loadButton.GetComponent<Image>().color = Color.gray;
            }
            tutorialButton.onClick.AddListener(ShowTutorial);
            backButton.onClick.AddListener(ShowTitleScreen);
            exitButton.onClick.AddListener(ExitGame);
            _audioManager = GetComponent<AudioManager>();
            StartCoroutine(FancyRoutine());
        }

        private IEnumerator FancyRoutine()
        {
            startButton.gameObject.SetActive(false);
            tutorialButton.gameObject.SetActive(false);
            loadButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(false);
            _audioManager.Play("LabyrinthOfDeath");
            // fade title in
            for (float i = 0; i < fadeDuration; i += Time.deltaTime)
            {
                // set color with i as alpha
                gameTitle.color = new Color(1, 1, 1, i);
                yield return null;
            }
            _audioManager.Play("Stinger");
            startButton.gameObject.SetActive(true);
            tutorialButton.gameObject.SetActive(true);
            loadButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(1f);
            
            _audioManager.Play("MenuMusic");
            
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        private void StartGame()
        {
            _audioManager.Stop("MenuMusic");
            SaveSystem.DeletePlayer();
            GameManager.Instance.LoadMainScene();
        }
    
        private void LoadGame()
        {
            _audioManager.Stop("MenuMusic");
            GameManager.Instance.LoadMainScene();
        }

        /// <summary>
        /// Shows the tutorial.
        /// </summary>
        private void ShowTutorial()
        {
            //We hide the title screen and display the tutorial
            titleScreen.SetActive(false);
            tutorialScreen.SetActive(true);
        }

        /// <summary>
        /// Shows the title screen.
        /// </summary>
        private void ShowTitleScreen()
        {
            //We hide the tutorial and display the title screen
            tutorialScreen.SetActive(false);
            titleScreen.SetActive(true);
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