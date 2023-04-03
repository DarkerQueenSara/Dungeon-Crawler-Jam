using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{


    public abstract class BaseState : MonoBehaviour
    {
        public abstract BaseState RunState();



    }
}