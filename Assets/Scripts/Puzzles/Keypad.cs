using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzles
{
    public class Keypad :MonoBehaviour
    {
        public KeypadUI graphics;
        public List<LockedDoor> door;

        [HideInInspector] public bool solved;


        private void Update()
        {
            if (!solved)
            {
                if (!(door[0].locked && door[1].locked))
                {
                    solved = true;
                }
                else
                {
                    solved = false;
                }
            }
        }

        public void StartPuzzle()
        {
            if (solved) return;
            graphics.gameObject.SetActive(true);
        }

        public void EndPuzzle()
        {
            door[0].locked = false;
            door[1].locked = false;
            Destroy(graphics.gameObject);
        }
    }
}