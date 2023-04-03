using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{


    public class StateMachine : MonoBehaviour
    {
        public BaseState currentState;

        public void RunStateMachine()
        {
            BaseState newState = currentState?.RunState();

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