using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
        //isto em principio troca items
        //mas se tivermos combinar items como Resi, talvez não queiramos isso
        /*else
        {
            InventoryItem thisItem = transform.GetChild(0).GetComponent<InventoryItem>();
            InventoryItem otherItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            thisItem.transform.SetParent(otherItem.transform.parent);
            otherItem.parentAfterDrag = transform;
        }*/
    }
}
