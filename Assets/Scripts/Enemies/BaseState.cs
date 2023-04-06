using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{


    public abstract class BaseState : MonoBehaviour
    {
        public bool isActing = false;
        public abstract BaseState RunState(Vector3 playerPos);



    }
}