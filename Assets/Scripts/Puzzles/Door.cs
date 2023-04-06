using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Items
{
    public class Door : MonoBehaviour
    {
        public void TeleportPlayer()
        {
            PlayerEntity.Instance.gameObject.transform.position = PlayerEntity.Instance.gameObject.transform.position +
                                                                  (PlayerEntity.Instance.gameObject.transform.forward *
                                                                  2);
        }
    }
}