using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {

        public void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount == 0)
            {
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                inventoryItem.parentAfterDrag = transform;
            }
            else
            {
               InventoryItem thisItem = transform.GetChild(0).GetComponent<InventoryItem>();
               InventoryItem otherItem = eventData.pointerDrag.GetComponent<InventoryItem>();
               thisItem.CombineItem(otherItem);
            }
        }
    }
}
