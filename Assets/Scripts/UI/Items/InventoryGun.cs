using Audio;
using Enemies;
using Player;
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
        private AudioManager _audioManager;

        public TextMeshProUGUI ammoText;
        
        public void Start()
        {
            currentAmmo = maxAmmo;
            _audioManager = GetComponent<AudioManager>();

        }

        private void Update()
        {
            ammoText.text = currentAmmo.ToString();
        }
        
        public override void UseItem()
        {
            if (currentAmmo > 0)
            {
                _audioManager.Play("shoot");

                currentAmmo--;

                RaycastHit hit;
                if (Physics.Raycast(PlayerEntity.Instance.transform.position, PlayerEntity.Instance.transform.forward, out hit, range))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Zombie"))
                        hit.collider.GetComponent<ZombieMelee>().DealDamageSelf(damage);
                    
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("DestructibleWall"))
                        hit.collider.GetComponent<DestructibleWall>().RecieveDamage();
                }
            }
            else
                Debug.Log("Out of ammo...");
        }
    }
}