using System;
using UI;
using Items;

namespace UI.Items
{
    public class ShotgunAmmo: InventoryStackable
    {
        private void Start()
        {
            item = ItemType.ShotgunAmmo; 
        }

        public override void UseItem()
        {
        }

        public override bool CombineItem(InventoryItem item)
        {
            if(item.item == ItemType.Shotgun)
            {
                return true;
            }  
            return false;
        }

    }
}