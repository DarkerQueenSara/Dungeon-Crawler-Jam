using System;
using Player;
using UnityEngine;

namespace Puzzles
{
    public class Bookshelf : MonoBehaviour
    {
        #region SingleTon

        /// <summary>
        /// Gets the sole instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Bookshelf Instance { get; private set; }

        /// <summary>
        /// Awakes this instance (if none have been created already).
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        #endregion
        
        [HideInInspector] public bool redBook;
        [HideInInspector] public bool blackBook;
        [HideInInspector] public bool blueBook;

        public LockedFinalDoor lockedDoor;

        public void Update()
        {
            if (redBook && blackBook && blueBook) lockedDoor.locked = false;
        }

        public void GetMessage()
        {
            int c = BooksLeft();
            if (c > 0)
            {
                PlayerHUD.Instance.AddMessage("There are " + c + " books missing from the shelf.");
            }
            else
            {
                PlayerHUD.Instance.AddMessage("The bookshelf is full. You feel something's been unlocked.");
            }
        }
        
        private int BooksLeft()
        {
            int c = 0;
            if (!redBook) c++;
            if (!blackBook) c++;
            if (!blueBook) c++;
            return c;
        }
    }
}