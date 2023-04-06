using Player;
using UnityEngine;

namespace Managers.Save_System
{
    [System.Serializable]
    public class PlayerData 
    {
        public int maxHealth;
        public int currentHealth;
        public float[] playerPosition;
        public float[] playerRotation;
        
        public PlayerData(PlayerEntity playerEntity)
        {
            currentHealth = playerEntity.health.currentHealth;
            maxHealth = playerEntity.health.maxHealth;
            Transform playerTransform = playerEntity.gameObject.transform;
            Vector3 playerPos = playerTransform.position;
            Vector3 playerRot = playerTransform.rotation.eulerAngles;
            playerPosition = new float[3];
            playerPosition[0] = playerPos.x;
            playerPosition[1] = playerPos.y;
            playerPosition[2] = playerPos.z;
            playerRotation = new float[3];
            playerRotation[0] = playerRot.x;
            playerRotation[1] = playerRot.y;
            playerRotation[2] = playerRot.z;
        }
    }
}