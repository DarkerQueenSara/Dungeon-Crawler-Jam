using UnityEngine;

namespace Player
{
    /// <summary>
    /// Manages the player's gold 
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class PlayerInventory : MonoBehaviour
    {
        /// <summary>
        /// Gets the current gold.
        /// </summary>
        /// <value>
        /// The current gold.
        /// </value>
        public int CurrentGold { get; private set; }
        
        private void Start()
        {
            CurrentGold = 0;
        }

        /// <summary>
        /// Increases the player's gold.
        /// </summary>
        /// <param name="value">The value.</param>
        public void IncreaseGold(int value)
        {
            CurrentGold += value;
            PlayerEntity.Instance.audioManager.Play("Coin");
        }

        /// <summary>
        /// Spends the gold.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SpendGold(int value)
        {
            CurrentGold -= value;
        }
    }
}