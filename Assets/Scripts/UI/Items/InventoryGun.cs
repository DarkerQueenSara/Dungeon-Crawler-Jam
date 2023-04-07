using Enemies;
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
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        // Deal damage to the enemy
                        //hit.collider.GetComponent<ZombieMelee>().DealDamage(damage);
                    }
                }
            }
            else
                Debug.Log("Out of ammo...");
        }
    }
}