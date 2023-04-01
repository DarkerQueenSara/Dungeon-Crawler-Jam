using Extensions;
using Player;
using UnityEngine;

namespace Items
{
    /// <summary>
    /// The currency the player picks up
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class Gold : MonoBehaviour
    {
        /// <summary>
        /// The value of the coin
        /// </summary>
        public int value;
        /// <summary>
        /// The player layer
        /// </summary>
        public LayerMask player;

        /// <summary>
        /// Called when [trigger enter2 d].
        /// </summary>
        /// <param name="col">The col.</param>
        public void OnTriggerEnter2D(Collider2D col)
        {
            //If the object that stepped into the coin is in the player layer, i.e., is the player
            if (player.HasLayer(col.gameObject.layer))
            {
                //Add gold to inventory and display a message
                PlayerEntity.Instance.inventory.IncreaseGold(value);
                PlayerHUD.Instance.AddMessage("Picked up " + value + " gold.");
                Destroy(gameObject);
            }
        }
    }
}