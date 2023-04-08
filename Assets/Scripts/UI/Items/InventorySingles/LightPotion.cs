using Items;
using Managers;
using Player;
using Unity.VisualScripting;
using UnityEngine;


namespace UI.Items.InventorySingles
{
    public class LightPotion: InventoryItem
    {
        public int lifeToRecover;
        
        public GameObject combinedPrefab;

        private void Start()
        {
            item = ItemType.LightPotion; 
            lifeToRecover = 10; 
        }

        public override void UseItem()
        {
            PlayerEntity.Instance.health.RestoreHealth(lifeToRecover);
            TurnManager.Instance.ProcessTurn(PlayerEntity.Instance.transform.position);
            Destroy(gameObject);

        }

        public override void CombineItem(InventoryItem item)
        {
            if(item.item == ItemType.PotionBoost)
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