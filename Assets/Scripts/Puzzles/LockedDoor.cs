namespace Puzzles
{
    public class LockedDoor : Door
    {
        public bool locked = true;

        public override void TeleportPlayer()
        {
            if (!locked)
            {
                base.TeleportPlayer();
            }
        }
    }
}