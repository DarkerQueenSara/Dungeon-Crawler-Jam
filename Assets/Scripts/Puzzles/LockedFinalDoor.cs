using Managers;
using Player;

namespace Puzzles
{
    public class LockedFinalDoor : LockedDoor
    {
        public override void TeleportPlayer()
        {
            if (!locked)
            {
                GameManager.Instance.LoadCredits();
            }
            else
            {
                PlayerHUD.Instance.AddMessage("'To unlock the door, seek knowledge' it says.");
            }
        }
    }
}