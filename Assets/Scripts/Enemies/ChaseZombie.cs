using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{


    public class ChaseZombie : BaseState
    {
        public bool isAttackRange = false;

        public bool OutOfChase = false;

        // Start is called before the first frame update
        public override BaseState RunState()
        {
            if (isAttackRange)
            {
                return new AttackZombie();
            }

            else if (OutOfChase)
            {
                return new IdleZombie();
            }

            return this;
        }


    }
}