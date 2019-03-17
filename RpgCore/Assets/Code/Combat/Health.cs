using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    /// <summary>
    /// Handles health of characters
    /// </summary>
    public class Health : MonoBehaviour {
        [SerializeField] private float _healthPoints = 100f;

        private const string dieTrigger = "die";

        private bool _isDead = false;
        public bool IsDead { get { return _isDead; } }

        /// <summary>
        /// Take damage from a fighter
        /// </summary>
        /// <param name="damage">Damage to take</param>
        public void TakeDamage(float damage) {
            _healthPoints = Mathf.Max(_healthPoints - damage, 0);
            if (_healthPoints == 0) {
                Die();
            }
        }

        private void Die() {
            if (_isDead) return;
            _isDead = true;
            GetComponent<Animator>().SetTrigger(dieTrigger);
        }
    }
}