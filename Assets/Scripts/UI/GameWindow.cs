using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Items;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class GameWindow: MonoBehaviour, IDropHandler, IPointerClickHandler
    {

        [SerializeField] protected RectTransform rawImageRectTrans;
        [SerializeField] protected Camera renderToTextureCamera;
        
        public List<GameObject> itemPrefabs;
        public LayerMask obstacles;
        public LayerMask environmentItems;

        private GameObject _clickedItem;
        
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
            if (toSpawn == null)
            {
                return;
            }
            //Get spawn position
            Physics.Raycast(posAhead, Vector3.down, out RaycastHit hit);
            float height = hit.distance;
            //Spawn the prefab at the right position
            Instantiate(toSpawn, posAhead + (Vector3.down * height), Quaternion.identity);
            //Destroy the item in the UI
            Destroy(inventoryItem.gameObject);
        }
        
        /// <summary>
        /// What happens when you click an object on the game window
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            Vector2 localPoint = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rawImageRectTrans, eventData.position, null, out localPoint);
            Vector2 normalizedPoint = Rect.PointToNormalized(rawImageRectTrans.rect, localPoint);
            Ray renderRay = renderToTextureCamera.ViewportPointToRay(normalizedPoint);
            if (Physics.Raycast(renderRay, out var raycastHit))
            {
                if (environmentItems.HasLayer(raycastHit.collider.gameObject.layer))
                {
                    _clickedItem = raycastHit.collider.gameObject;
                    PickUpItem(_clickedItem);
                }
            }
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

        private void PickUpItem(GameObject item)
        {
            InventoryManager.Instance.AddItem(item);
            _clickedItem = null;
        }
    }
}