using Items;

namespace UI.Items
{
    public class HandgunAmmo: InventoryStackable
    {
        private void Start()
        {
            item = ItemType.HandgunAmmo; 
        }

        public override void UseItem()
        {
        }

        public override void CombineItem(InventoryItem item)
        {
            if(item.item == ItemType.Handgun)
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
                    Destroy(this);
                }
                
            }  
            
        }
    }
}