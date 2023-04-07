using Player;

namespace UI.Items.InventorySingles
{
    public class CodeCard : InventoryItem
    {
        public override void UseItem()
        {
            PlayerHUD.Instance.AddMessage("It's a card. It says 1-9-9-6.");
        }

        public override void CombineItem(InventoryItem item)
        {
        }
    }
}