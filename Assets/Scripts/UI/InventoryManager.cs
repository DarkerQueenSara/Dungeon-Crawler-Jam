using System.Collections.Generic;
using System.Linq;
using Items;
using Player;
using UnityEngine;

namespace UI
{
    public class InventoryManager: MonoBehaviour
    {
        #region SingleTon
        /// <summary>
        /// Gets the sole instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static InventoryManager Instance { get; private set; }

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
        
        public List<GameObject> inventoryItemPrefabs;

        public void AddItem(GameObject environmentItem)
        {
            GameObject freeSlot = GetFirstFreeSlot();
            if (freeSlot == null)
            {
                PlayerHUD.Instance.AddMessage("You don't have room in your inventory");
                return;
            }

            Instantiate(GetRightItem(environmentItem.GetComponent<EnvironmentItem>().item), freeSlot.transform.position, Quaternion.identity, freeSlot.transform);
            Destroy(environmentItem);
        }
        
        private GameObject GetFirstFreeSlot()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).childCount == 0)
                {
                    return transform.GetChild(i).gameObject;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Get the right prefab to instantiate in the map
        /// </summary>
        /// <param name="itemType">The type of the item that was dropped</param>
        /// <returns></returns>
        private GameObject GetRightItem(ItemType itemType)
        {
            return inventoryItemPrefabs.FirstOrDefault(prefab => prefab.GetComponent<EnvironmentItem>().item == itemType);
        }
    }
}