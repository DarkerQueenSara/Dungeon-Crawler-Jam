using System;
using UI;
using Items;
using Player;

namespace UI.Items
{
    public class PotionBoost: InventoryItem
    {
        public int lifeToRecover;

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
                item.item = ItemType.StrongPotion;
                Destroy(this);
            }  
        }

    }
}