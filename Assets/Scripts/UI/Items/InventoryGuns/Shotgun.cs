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