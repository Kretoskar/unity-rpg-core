using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core {
    /// <summary>
    /// Handles health of characters
    /// </summary>
    public class Health : MonoBehaviour, ISaveable {
        [SerializeField]
        [Range(0,9999)]
        private float _healthPoints = 100f;

        private bool _isDead = false;
        public bool IsDead { get { return _isDead; } }

        private const string dieTrigger = "die";

        /// <summary>
        /// Take damage from a fighter
        /// </summary>
        /// <param name="damage">Damage to take</param>
        public void TakeDamage(float damage) {
            _healthPoints = Mathf.Max(_healthPoints - damage, 0);
            if (_healthPoints <= 0) {
                Die();
            }
        }

        /// <summary>
        /// Capture state to save
        /// </summary>
        /// <returns>State to save</returns>
        public object CaptureState() {
            return _healthPoints;
        }

        /// <summary>
        /// Restore state to load
        /// </summary>
        /// <param name="state">State to load</param>
        public void RestoreState(object state) {
            _healthPoints = (float)state;
            if (_healthPoints <= 0) {
                Die();
            }
        }

        /// <summary>
        /// Behaviour on death
        /// </summary>
        private void Die() {
            if (_isDead) return;
            _isDead = true;
            GetComponent<Animator>().SetTrigger(dieTrigger);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}