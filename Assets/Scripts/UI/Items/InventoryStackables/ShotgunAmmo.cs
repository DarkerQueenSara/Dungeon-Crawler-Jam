using System;
using UI;
using Items;
using UI.Items.InventoryGuns;

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

        public override void CombineItem(InventoryItem item)
        {
            if(item.item == ItemType.Shotgun)
            {
                InventoryGun gun = (InventoryGun)item;
                this.amount = this.amount - (gun.maxAmmo - gun.currentAmmo);
                if (amount > 0)
                {
                    gun.currentAmmo = gun.maxAmmo;
                }
                else
                {
                    gun.currentAmmo = gun.maxAmmo + this.amount;
                    Destroy(this.gameObject);
                }


            }  
            
        }

    }
}