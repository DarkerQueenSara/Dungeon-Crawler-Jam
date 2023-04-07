namespace Puzzles
{
    public class LockedDoor : Door
    {
        public bool locked;

        public override void TeleportPlayer()
        {
            if (!locked)
            {
                base.TeleportPlayer();
            }
        }
    }
}