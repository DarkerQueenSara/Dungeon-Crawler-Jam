using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Managers;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Enemies
{


    public class AttackZombie : BaseState
    {
        private bool isAttackRange = true;
        public ChaseZombie  chase;
        public ZombieMelee owner;

        public StateMachine manager;

        // Start is called before the first frame update
        public override BaseState RunState(Vector3 playerPos)
        {
            transform.position = transform.root.position;
            InRangeOfAttack(playerPos);
            if (!isAttackRange)
            {
                manager.stateID = 1;
                return chase;
            }

            AttackThePlayer();
            manager.stateID = 2;
            return this;
        }

        private void InRangeOfAttack(Vector3 playerPos)
        {
            Vector3[] fourDirections = {Vector3.forward, Vector3.forward * -1, Vector3.right, Vector3.right * -1};
            foreach (var direction in fourDirections)
            {
                if (Math.Abs((transform.position + direction).x - playerPos.x) <= 0.1f &&
                    Math.Abs((transform.position + direction).z - playerPos.z) <= 0.1f)
                {
                    isAttackRange = true;
                    return;
                }
            }
            isAttackRange = false;
            return;
        }

        private void AttackThePlayer()
        {
            PlayerEntity.Instance.health.DealDamage(owner.damage);
            return;
        }

    }
}