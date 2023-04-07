using System;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// A serializable sound class to use in the inspector in conjuction with the AudioManager
    /// </summary>
    [Serializable]
    public class Sound
    {
        /// <summary>
        /// The name of the sound
        /// </summary>
        public string name;
        /// <summary>
        /// The sound clip
        /// </summary>
        public AudioClip clip;

        /// <summary>
        /// The audio volume
        /// </summary>
        [Range(0f, 1f)] public float volume = 1f;

        /// <summary>
        /// The audio pitch
        /// </summary>
        [Range(0.1f, 3f)] public float pitch = 1f;

        [Range(0.0f, 1.0f)] public float spatialBlend = 0.0f;
        
        /// <summary>
        /// If the sound loops
        /// </summary>
        public bool loop;

        /// <summary>
        /// The Unity AudioSource (created in code through the AudioManager)
        /// </summary>
        [HideInInspector] public AudioSource source;

        /// <summary>
        /// Sets the AudioSource.
        /// </summary>
        /// <param name="audioSource">The AudioSource.</param>
        public void SetSource(AudioSource audioSource)
        {
            audioSource.clip = clip;

            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.loop = loop;
            audioSource.spatialBlend = spatialBlend;
            source = audioSource;
        }

        /// <summary>
        /// Plays this source's sound.
        /// </summary>
        public void Play()
        {
            source.Play();
        }
        
        /// <summary>
        /// Stops this source's sound.
        /// </summary>
        public void Stop()
        {
            source.Stop();
        }

        /// <summary>
        /// Determines whether this source's sound is playing.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this source's sound is playing; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPlaying()
        {
            return source.isPlaying;
        }
    }
}