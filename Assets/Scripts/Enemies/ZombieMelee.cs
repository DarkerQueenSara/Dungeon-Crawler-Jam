using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{


    public class ZombieMelee : MonoBehaviour
    {
        public int health;
        public int damage;
        private StateMachine manager;
        private AudioManager audioManager;  

        private void Start()
        {
            manager = GetComponent<StateMachine>();
            audioManager = GetComponent<AudioManager>();
        }

        

        public void DealDamageSelf(int damageNew)
        {
            health += -damageNew;
            manager.animator.SetBool("isDamaged", true);
            audioManager.Play("zombie-hurt");
            
            if (health <= 0)
            {
                
                manager.animator.SetBool("isDead", true);
                
                Invoke("DeathZombie", 1f);
            }
            manager.animator.SetBool("isDamaged", false);
        }


        private void DeathZombie()
        {
            audioManager.Play("zombie-dead");
            TurnManager.Instance._enemiesInMap.Remove(gameObject.GetComponent<StateMachine>());
            Destroy(gameObject);
        }
    }
}