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
        public virtual void TeleportPlayer()
        {
            PlayerEntity.Instance.movement.LockMovement();
            if (mainCamera == null) mainCamera = Camera.main;
            if (mainCamera != null) mainCamera.enabled = false;
            _spawned = Instantiate(animatedDoor);
            PlayerEntity.Instance.TeleportPlayer();
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