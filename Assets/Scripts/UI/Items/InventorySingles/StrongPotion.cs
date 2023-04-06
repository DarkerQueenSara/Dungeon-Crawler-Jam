using Items;
using Player;

namespace UI.Items.InventorySingles
{
    public class StrongPotion: InventoryItem
    {
        public int lifeToRecover;

        private void Start()
        {
            item = ItemType.StrongPotion; 
            lifeToRecover = 50; 
        }

        public override void UseItem()
        {
            PlayerEntity.Instance.health.RestoreHealth(lifeToRecover);
            Destroy(this);
        }

        public override void CombineItem(InventoryItem item)
        { 
        }

    }
}