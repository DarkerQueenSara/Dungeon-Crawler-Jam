using UnityEngine;

namespace Items
{
    public abstract class EnvironmentItem : MonoBehaviour
    {
        public ItemType item;
        public bool stackable;
        public int amount;
    }
}