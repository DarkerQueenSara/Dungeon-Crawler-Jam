using Items;
using Player;

namespace UI.Items.InventorySingles
{
    public class LightPotion: InventoryItem
    {
        public int lifeToRecover;

        private void Start()
        {
            item = ItemType.LightPotion; 
            lifeToRecover = 10; 
        }

        public override void UseItem()
        {
            PlayerEntity.Instance.health.RestoreHealth(lifeToRecover);
            Destroy(this);

        }

        public override void CombineItem(InventoryItem item)
        {
            if(item.item == ItemType.PotionBoost)
            {
                this.item = ItemType.StrongPotion;
                Destroy(item);
            }  
            
        }

    }
}