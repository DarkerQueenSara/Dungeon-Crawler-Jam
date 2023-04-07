using Items;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.Items.InventorySingles
{
    public class PotionBoost: InventoryItem
    {
        
        public GameObject combinedPrefab;
        private void Start()
        {
            item = ItemType.PotionBoost;
        }

        public override void UseItem()
        {
        }

        public override void CombineItem(InventoryItem item)
        {
            if(item.item == ItemType.LightPotion)
            {
                GameObject newObject = Instantiate(combinedPrefab, transform.position, Quaternion.identity);
                newObject.transform.SetParent(transform.parent);
                newObject.transform.localScale = Vector3.one;
                
                Destroy(item.gameObject);
                Destroy(gameObject);
            }  
        }

    }
}