using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{


    public class AttackZombie : BaseState
    {
        public bool isAttackRange = true;

        // Start is called before the first frame update
        public override BaseState RunState()
        {
            if (!isAttackRange)
            {
                return new ChaseZombie();
            }

            return this;
        }


    }
}