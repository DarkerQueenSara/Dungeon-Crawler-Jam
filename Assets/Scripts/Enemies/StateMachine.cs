using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{


    public class StateMachine : MonoBehaviour
    {
        public BaseState currentState;
        public Animator animator;
        public int stateID;
        private void Start()
        {
            animator = GetComponent<Animator>();
            stateID = 0;
        }

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

            switch (stateID)
            {
                case 0:
                    animator.SetBool("isChasing", false);
                    break;
                case 1 :
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isChasing", true);
                    break;
                case 2 :
                    animator.SetBool("isChasing", false);
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isAttacking", true);
                    animator.SetBool("isIdle", true);
                    break;

            }
        }
    }

}