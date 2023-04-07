using UnityEngine;

namespace Puzzles
{
    public class Keypad :MonoBehaviour
    {
        public KeypadUI graphics;
        public LockedDoor door;

        [HideInInspector] public bool solved;
        
        public void StartPuzzle()
        {
            if (solved) return;
            graphics.gameObject.SetActive(true);
        }

        public void EndPuzzle()
        {
            door.locked = false;
            Destroy(graphics.gameObject);
        }
    }
}