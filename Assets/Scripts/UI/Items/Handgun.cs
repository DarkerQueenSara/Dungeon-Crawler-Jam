using System;
using UI;
using Items;
using UnityEngine;

namespace UI.Items
{
    public class Handgun: InventoryItem
    {
        public int maxAmmo;
        private int currentAmmo;

        private void Start()
        {
            item = ItemType.Handgun; 
            currentAmmo = maxAmmo; 
        }

        public override void UseItem()
        {
            currentAmmo--;
            Debug.Log("Bang!");
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
