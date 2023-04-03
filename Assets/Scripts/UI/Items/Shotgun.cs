using System;
using UI;
using Items;

namespace UI.Items
{
    public class Shotgun: InventoryItem
    {
        public int maxAmmo;
        private int currentAmmo;

        private void Start()
        {
            item = ItemType.Shotgun; 
            currentAmmo = maxAmmo; 
        }

        public override void UseItem()
        {
            currentAmmo--;
            //TODO shoot

        }

        public override bool CombineItem(InventoryItem item)
        {
            if(item.item == ItemType.ShotgunAmmo)
            {
                return true;
            }  
            return false;
        }

    }
}