using Audio;
using Extensions;
using UnityEngine;

namespace Puzzles
{
    /// <summary>
    /// The portal that ends the level
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class EndPortal : MonoBehaviour
    {
        /// <summary>
        /// If the player has stepped into the portal
        /// </summary>
        [HideInInspector] public bool hasPlayer;
        /// <summary>
        /// The AudioManager
        /// </summary>
        [HideInInspector] public AudioManager audioManager;
        /// <summary>
        /// The player's layer
        /// </summary>
        public LayerMask player;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            audioManager = GetComponent<AudioManager>();
            audioManager.Play("Portal");
        }

        /// <summary>
        /// Called when [trigger enter2 d].
        /// </summary>
        /// <param name="col">The col.</param>
        public void OnTriggerEnter2D(Collider2D col)
        {
            //If the object that stepped into the portal is in the player layer, i.e., is the player
            if (player.HasLayer(col.gameObject.layer))
            {
                //Play a sound, and end the level/game
                audioManager.Play("enterPortal");
                //TODO fix the fact this does nothing

                Destroy(gameObject);
            }
        }
    }
}