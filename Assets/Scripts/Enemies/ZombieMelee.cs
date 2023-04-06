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
        public float health = 100f;
        public float damage = 25f;
        public StateMachine state;


        /*private void Awake()
        {
            state.positionCurrentState =  transform.position;
        }

        private void Update()
        {
            transform.position = state.positionCurrentState;
        }*/
    }
}