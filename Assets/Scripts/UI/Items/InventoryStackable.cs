using System;
using TMPro;
using UnityEngine;

namespace UI.Items
{
    public abstract class InventoryStackable : InventoryItem
    {
        public int amount;
        public TextMeshProUGUI amountText;

        public void Update()
        { 
            amountText.text = amount.ToString();
        }
    }
}