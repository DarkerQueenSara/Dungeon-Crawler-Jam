using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzles
{
    public class FadeImage : MonoBehaviour
    {
        public float fadeDuration;
        private Image _fadeImage;
        private bool _started;
        
        private void Start()
        {
            _fadeImage = GetComponent<Image>();
            _started = true;
        }

        private void OnEnable()
        {
            if (!_started) Start();
            _fadeImage.color = Color.black;
            StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            // loop over 1 second backwards
            for (float i = fadeDuration; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                _fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
            //TODO return control to player
            gameObject.SetActive(false);
        }
    }
}