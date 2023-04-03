using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{


    public class IdleZombie : BaseState
    {
        public bool isInRange = false;

        // Start is called before the first frame update
        public override BaseState RunState()
        {
            if (isInRange)
            {
                return new ChaseZombie();
            }

            return this;
        }


    }
}