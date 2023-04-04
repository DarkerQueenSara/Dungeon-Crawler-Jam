
using Items;

namespace UI.Items.InventoryGuns
{
    public class Handgun: InventoryGun
    {

        public void Start()
        {
            base.Start();
            item = ItemType.Handgun; 
        }

        public override void UseItem()
        {
            base.UseItem();
            //TODO shoot
        }

        public override bool CombineItem(InventoryItem item)
        {
            if(item.item == ItemType.HandgunAmmo)
            {
                return true;
            }  
            return false;
        }

    }
} 
