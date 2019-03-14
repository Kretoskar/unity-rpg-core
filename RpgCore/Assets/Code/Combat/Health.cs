using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    /// <summary>
    /// Handles health of characters
    /// </summary>
    public class Health : MonoBehaviour {
        [SerializeField] private float health = 100f;

        /// <summary>
        /// Take damage from a fighter
        /// </summary>
        /// <param name="damage">Damage to take</param>
        public void TakeDamage(float damage) {
            health = Mathf.Max(health - damage, 0);
            print(health);
        }
    }
}