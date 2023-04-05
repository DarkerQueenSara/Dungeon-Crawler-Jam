using Items;

namespace UI.Items.InventoryGuns
{
    public class Shotgun: InventoryGun
    {
        
        
        private void Start()
        {
            base.Start();
            item = ItemType.Shotgun;
        }

        public override void UseItem()
        {
            base.UseItem();
        }

        public override void CombineItem(InventoryItem item)
        {
            if(item.item == ItemType.ShotgunAmmo)
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