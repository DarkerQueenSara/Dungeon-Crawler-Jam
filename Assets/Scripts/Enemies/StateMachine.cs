using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{


    public class StateMachine : MonoBehaviour
    {
        public BaseState currentState;
        public ZombieMelee owner;

        public void RunStateMachine(Vector3 playerPos)
        {
            BaseState newState = currentState?.RunState(playerPos);
            if (currentState != null)
            {
                ChangeState(newState);
            }

        }

        public void ChangeState(BaseState newState)
        {
            currentState = newState;
        }
    }

}