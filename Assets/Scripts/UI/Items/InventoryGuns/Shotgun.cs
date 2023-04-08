using Audio;
using Items;

namespace UI.Items.InventoryGuns
{
    public class Shotgun: InventoryGun
    {
        //private AudioManager _audioManager;

        
        private void Start()
        {
            //_audioManager = GetComponent<AudioManager>();
            base.Start();
            item = ItemType.Shotgun;
            damage = 100;
            range = 4;
        }

        public override void UseItem()
        {
            base.UseItem();
            //_audioManager.Play("shoot");
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