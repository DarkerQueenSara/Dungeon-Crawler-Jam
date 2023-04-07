using System;
using Audio;
using Extensions;
using Player;
using UnityEngine;

namespace Puzzles
{
    /// <summary>
    /// The portal that ends the level
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class Portal : MonoBehaviour
    {
        /// <summary>
        /// If the player has stepped into the portal
        /// </summary>
        public bool hasPlayer;
        /// <summary>
        /// The AudioManager
        /// </summary>
        [HideInInspector] public AudioManager audioManager;
        /// <summary>
        /// The player's layer
        /// </summary>
        public LayerMask player;

        public bool invisible;

        private void Start()
        {
            audioManager = GetComponent<AudioManager>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (player.HasLayer(other.gameObject.layer))
            {
                //Play enter portal sound
                audioManager.Play("enter-portal");
                hasPlayer = true;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            //If the object that stepped into the portal is in the player layer, i.e., is the player
            if (player.HasLayer(other.gameObject.layer))
            {
                hasPlayer = false;
            }
        }
    }
}