using Enemies;
using TMPro;
using UnityEngine;

namespace UI.Items
{
    public abstract class InventoryGun : InventoryItem
    {
        public int damage;
        public int maxAmmo;
        public int range;
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
                if (Physics.Raycast(transform.position, transform.forward, out hit, range))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        // Deal damage to the enemy
                        hit.collider.GetComponent<ZombieMelee>().DealDamageSelf(damage);
                        Debug.Log("Hit");
                    }
                }
            }
            else
                Debug.Log("Out of ammo...");
        }
    }
}