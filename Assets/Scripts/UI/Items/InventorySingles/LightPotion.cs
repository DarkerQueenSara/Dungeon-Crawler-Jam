using System;
using UI;
using Items;
using Player;

namespace UI.Items
{
    public class LightPotion: InventoryItem
    {
        public int lifeToRecover;

        private void Start()
        {
            item = ItemType.LightPotion; 
            lifeToRecover = 10; 
        }

        public override void UseItem()
        {
            PlayerEntity.Instance.health.RestoreHealth(lifeToRecover);

        }

        public override bool CombineItem(InventoryItem item)
        {
            if(item.item == ItemType.PotionBoost)
            {
                return true;
            }  
            return false;
        }

    }
}