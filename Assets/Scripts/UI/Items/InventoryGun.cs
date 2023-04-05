using TMPro;
using UnityEngine;

namespace UI.Items
{
    public abstract class InventoryGun : InventoryItem
    {
        public int damage;
        public int maxAmmo;
        [HideInInspector] public int currentAmmo;

        public TextMeshProUGUI ammoText;
        
        public void Start()
        {
            currentAmmo = maxAmmo;
        }

        private void Update()
        {
            ammoText.text = currentAmmo.ToString();
        }
        
        public override void UseItem()
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;
                Debug.Log("Bang!");
                //TODO shoot
            }
            else
                Debug.Log("Out of ammo...");
        }
    }
}