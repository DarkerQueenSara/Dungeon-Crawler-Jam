using System;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// A class that allows gameobjects to play sounds
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class AudioManager : MonoBehaviour
    {
        /// <summary>
        /// List of sounds this Game Object will play
        /// </summary>
        public Sound[] sounds;

        /// <summary>
        /// When the Game Object awakes, adds an AudioSource for each sound.
        /// </summary>
        private void Awake()
        {
            foreach (var s in sounds) s.SetSource(gameObject.AddComponent<AudioSource>());
        }

        /// <summary>
        /// Play a sound
        /// </summary>
        /// <param name="soundName">The name of the sound to play</param>
        public void Play(string soundName)
        {
            var s = Array.Find(sounds, sound => sound.name == soundName);
            if (s == null)
            {
                Debug.LogWarning("Sound " + soundName + " not found!");
                return;
            }

            if (s.IsPlaying()) return;
            s.Play();
        }

        /// <summary>
        /// Stop playing a sound
        /// </summary>
        /// <param name="soundName">The name of the sound to stop</param>
        public void Stop(string soundName)
        {
            var s = Array.Find(sounds, sound => sound.name == soundName);
            if (s == null)
            {
                Debug.LogWarning("Sound " + soundName + " not found!");
                return;
            }

            if (!s.IsPlaying()) return;
            s.Stop();
        }

        /// <summary>
        /// Determines whether the specified sound name is playing.
        /// </summary>
        /// <param name="soundName">Name of the sound.</param>
        /// <returns>
        ///   <c>true</c> if the specified sound name is playing; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPlaying(string soundName)
        {
            var s = Array.Find(sounds, sound => sound.name == soundName);
            if (s != null) return s.IsPlaying();
            Debug.LogWarning("Sound " + soundName + " not found!");
            return false;
        }
    }
}