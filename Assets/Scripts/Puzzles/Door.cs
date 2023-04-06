using Player;
using UnityEngine;

namespace Puzzles
{
    public class Door : MonoBehaviour
    {
        public float animTime;
        public GameObject animatedDoor;
        public FadeImage fadeImage;

        public Camera mainCamera;
        
        private GameObject _spawned;
        public void TeleportPlayer()
        {
            if (mainCamera == null) mainCamera = Camera.main;
            mainCamera.enabled = false;
            _spawned = Instantiate(animatedDoor);
            PlayerEntity.Instance.TeleportPlayer();
            PlayerEntity.Instance.movement.LockMovement();
            Invoke(nameof(FinishAnimation), animTime);
        }

        private void FinishAnimation()
        {
            mainCamera.enabled = true;
            fadeImage.gameObject.SetActive(true);
            Destroy(_spawned);
        }
    }
}