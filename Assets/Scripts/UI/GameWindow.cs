using System.Collections.Generic;
using System.Linq;
using Items;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class GameWindow: MonoBehaviour, IDropHandler
    {

        public List<GameObject> itemPrefabs;
        public LayerMask obstacles;
        
        /// <summary>
        /// What happens when you drop an object on the game window
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {
            Vector3 posAhead = PlayerEntity.Instance.GetPositionAhead();
            Collider[] col =
                Physics.OverlapBox(posAhead, new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity, obstacles);
            if (col.Length > 0)
            {
                PlayerHUD.Instance.AddMessage("Can't drop item ahead.");
                return;
            }
            //We get the inventory item we are dropping
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            //Check which prefab to spawn in the world
            //Because when the player discards an item, it is left on the floor ahead of them
            GameObject toSpawn = GetRightItem(inventoryItem.item);
            //Get spawn position
            Physics.Raycast(posAhead, Vector3.down, out RaycastHit hit);
            float height = hit.distance;
            //Spawn the prefab at the right position
            Instantiate(toSpawn, posAhead + (Vector3.down * height), Quaternion.identity);
            //Destroy the item in the UI
            Destroy(inventoryItem.gameObject);
        }

        /// <summary>
        /// Get the right prefab to instantiate in the map
        /// </summary>
        /// <param name="itemType">The type of the item that was dropped</param>
        /// <returns></returns>
        private GameObject GetRightItem(ItemType itemType)
        {
            return itemPrefabs.FirstOrDefault(prefab => prefab.GetComponent<EnvironmentItem>().item == itemType);
        }
    }
}