using System;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    /// <summary>
    /// The class that manages the player's health.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class PlayerHealth : MonoBehaviour
    {
        /// <summary>
        /// The maximum health
        /// </summary>
        public int maxHealth;
        /// <summary>
        /// The current health
        /// </summary>
        [HideInInspector] public int currentHealth;

        /// <summary>
        /// The layers of objects that damage the player
        /// </summary>
        public LayerMask damageables;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            currentHealth = maxHealth;
        }

        /// <summary>
        /// Checks if the player has damaging entities around it
        /// </summary>
        /// <returns>The number of damaging entities in contact with the player</returns>
        public int CheckDamage()
        {
            var c = 0;

            Vector3[] fourDirections = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };

            foreach (var direction in fourDirections)
                if (Physics2D.OverlapBox(transform.position + direction, new Vector2(0.5f, 0.5f), 0, damageables) !=
                    null)
                    c++;

            return c;
        }

        /// <summary>
        /// Deals damage to the player.
        /// </summary>
        /// <param name="damage">The amount of damage.</param>
        public void DealDamage(int damage)
        {
            PlayerEntity.Instance.audioManager.Play("Damage");
            PlayerHUD.Instance.AddMessage("The zombie dealt + " + damage + " damage.");
            currentHealth =
                Math.Clamp(
                    Mathf.RoundToInt(currentHealth - damage),
                    0, maxHealth);
            if (currentHealth == 0)
                Die();
        }

        /// <summary>
        /// Restores some health.
        /// </summary>
        /// <param name="heal">The amount to restore.</param>
        public void RestoreHealth(int heal)
        {
            currentHealth = Math.Clamp(currentHealth + heal, 0, maxHealth);
            PlayerHUD.Instance.AddMessage("You healed yourself for + " + heal + " HP.");
        }

        /// <summary>
        /// Ends the game.
        /// Loads game over scene.
        /// </summary>
        private static void Die()
        {
            GameManager.Instance.LoadCredits();
        }
    }
}