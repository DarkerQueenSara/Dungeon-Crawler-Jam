using System;
using UI;
using Items;

namespace UI.Items
{
    public class HandgunAmmo: InventoryItem
    {
        private void Start()
        {
            item = ItemType.HandgunAmmo; 
        }

        public override void UseItem()
        {
        }

        public override bool CombineItem(InventoryItem item)
        {
            if(item.item == ItemType.Handgun)
            {
                return true;
            }  
            return false;
        }

    }
}