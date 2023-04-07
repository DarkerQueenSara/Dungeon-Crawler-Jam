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
        [HideInInspector] public bool hasPlayer;
        /// <summary>
        /// The AudioManager
        /// </summary>
        [HideInInspector] public AudioManager audioManager;
        /// <summary>
        /// The player's layer
        /// </summary>
        public LayerMask player;

        public bool invisible;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            audioManager = GetComponent<AudioManager>();
        }

        /// <summary>
        /// Called when [trigger enter2 d].
        /// </summary>
        /// <param name="col">The col.</param>
        public void OnTriggerStay2D(Collider2D col)
        {
            Vector3 playerPos = PlayerEntity.Instance.gameObject.transform.position;
            //If the object that stepped into the portal is in the player layer, i.e., is the player
            if (player.HasLayer(col.gameObject.layer) && 
                (Math.Abs(playerPos.x - transform.position.x) < 0.01f || Math.Abs(playerPos.z - transform.position.z) <= 0.01f))
            {
                //Play a sound, and end the level/game
                audioManager.Play("enter-portal");

                if (playerPos.y > 5)
                {
                    PlayerEntity.Instance.transform.position += Vector3.up * 10; 
                    //Check World 1 sound
                    if(audioManager.IsPlaying("world1-default-ambience")) // this stops the audio coming from TurnManager? or just checks for Portal ? 
                        audioManager.Stop("world1-default-ambience");
                    //Start World 2 sound
                    audioManager.Play("world2-default-ambience");
                }
                else
                {
                    PlayerEntity.Instance.transform.position += Vector3.down * 10;
                    //Check World 2 sound
                    if(audioManager.IsPlaying("world2-default-ambience"))
                        audioManager.Stop("world2-default-ambience");
                    //Start World 1 sound
                    audioManager.Play("world1-default-ambience");
                }

                if (invisible)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}