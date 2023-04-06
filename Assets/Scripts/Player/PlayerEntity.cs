using Audio;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// A class to easily hold all the other classes used by the player
    /// It's best to split each part of the player into various classes to avoid bloated methods
    /// Or gigantic classes.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class PlayerEntity : MonoBehaviour
    {
        #region SingleTon

        /// <summary>
        /// Gets the sole instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static PlayerEntity Instance { get; private set; }

        /// <summary>
        /// Awakes this instance (if none exist already).
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        #endregion
        
        /// <summary>
        /// The PlayerHealth
        /// </summary>
        [HideInInspector] public PlayerHealth health;
        /// <summary>
        /// The PlayerMovement
        /// </summary>
        [HideInInspector] public PlayerMovement movement;
        /// <summary>
        /// The animator
        /// </summary>
        [HideInInspector] public Animator animator;
        /// <summary>
        /// The AudioManager
        /// </summary>
        [HideInInspector] public AudioManager audioManager;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            //Get all components from the GameObject
            health = GetComponent<PlayerHealth>();
            movement = GetComponent<PlayerMovement>();
            //animator = GetComponent<Animator>();
            //audioManager = GetComponent<AudioManager>();
        }

        public Vector3 GetPositionAhead()
        {
            return transform.position + transform.forward;
        }

        public void TeleportPlayer()
        {
            transform.position += transform.forward * 2.0f;
        }
    }
}