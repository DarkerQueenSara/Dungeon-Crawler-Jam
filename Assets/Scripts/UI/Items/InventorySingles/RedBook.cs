using Items;
using Player;
using Puzzles;
using UnityEngine;

namespace UI.Items.InventorySingles
{
    public class RedBook : InventoryItem
    {
        public LayerMask bookshelf;

        public void Start()
        {
            item = ItemType.RedBook;
        }

        public override void UseItem()
        {
            if (Physics.OverlapBox(PlayerEntity.Instance.GetPositionAhead(), new Vector3(1f, 3f, 1f),
                    Quaternion.identity, bookshelf).Length > 0)
            {
                Bookshelf.Instance.redBook = true;
            }
        }

        public override void CombineItem(InventoryItem item)
        {
            //do nothing
        }
    }
}