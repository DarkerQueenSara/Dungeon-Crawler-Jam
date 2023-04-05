using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Enemies
{


    public class AttackZombie : BaseState
    {
        public bool isAttackRange = true;
        
        public ChaseZombie  chase;

        // Start is called before the first frame update
        public override BaseState RunState(Vector3 playerPos)
        {
            if (!isAttackRange)
            {
                return chase;
            }

            return this;
        }

        private void InRangeOfAttack(Vector3 playerPos)
        {
            Vector3[] fourDirections = {Vector3.forward, Vector3.forward * -1, Vector3.right, Vector3.right * -1};
            foreach (var direction in fourDirections)
            {
                if (Math.Abs((transform.position + direction).x - playerPos.x) <= 0.1f &&
                    Math.Abs((transform.position + direction).y - playerPos.y) <= 0.1f)
                {
                    isAttackRange = true;
                    return;
                }
            }
            isAttackRange = false;
            return;
        }


    }
}