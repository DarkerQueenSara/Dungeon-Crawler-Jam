using Audio;
using Enemies;
using Extensions;
using UnityEngine;

namespace Items
{
    /// <summary>
    /// The trap the player uses to catch gremlins
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class Trap : MonoBehaviour
    {
        /// <summary>
        /// If a gremlin has stepped into the trap
        /// </summary>
        [HideInInspector] public bool hasGremlin;
        /// <summary>
        /// The caught gremlin
        /// </summary>
        [HideInInspector] public Gremlin caughtGremlin;
        /// <summary>
        /// The gremlins layers
        /// </summary>
        public LayerMask gremlins;
        /// <summary>
        /// The AudioManager
        /// </summary>
        private AudioManager _audioManager;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            _audioManager = GetComponent<AudioManager>();
        }

        /// <summary>
        /// Called when [trigger enter2 d].
        /// </summary>
        /// <param name="col">The col.</param>
        public void OnTriggerEnter2D(Collider2D col)
        {
            //If the object that stepped into the trap is in the gremlin layer, i.e., is a gremlin
            if (gremlins.HasLayer(col.gameObject.layer))
            {
                //Play a sound and store the gremlin (for disposal in the TurnManager)
                _audioManager.Play("Trap");
                hasGremlin = true;
                caughtGremlin = col.gameObject.GetComponent<Gremlin>();
            }
        }
    }
}