using UnityEngine;

namespace Items
{
    public class EnvironmentItem : MonoBehaviour
    {
        public ItemType item;

        public float rotateSpeed = 20f;

        private void Update()
        {
            transform.Rotate(0,rotateSpeed*Time.deltaTime,0);
        }
    }
}