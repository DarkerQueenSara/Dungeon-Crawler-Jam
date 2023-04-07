
using Items;

namespace UI.Items.InventoryGuns
{
    public class Handgun: InventoryGun
    {

        public void Start()
        {
            base.Start();
            item = ItemType.Handgun;
            damage = 50;
            range = 8;
        }

        public override void UseItem()
        {
            base.UseItem();
        }

        public override void CombineItem(InventoryItem item)
        {
            if(item.item == ItemType.HandgunAmmo)
            {
                InventoryStackable ammo = (InventoryStackable)item;
                ammo.amount = ammo.amount - (this.maxAmmo - this.currentAmmo);
                if (ammo.amount > 0)
                {
                    this.currentAmmo = this.maxAmmo;
                }
                else
                {
                    this.currentAmmo = this.maxAmmo + ammo.amount;
                    Destroy(item.gameObject);
                }


            }  
            
        }

    }
} 
