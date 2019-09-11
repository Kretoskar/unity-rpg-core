using RPG.Control;
using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core {
    /// <summary>
    /// Handles health of characters
    /// </summary>
    public class Health : MonoBehaviour, ISaveable {
        [SerializeField]
        [Range(0, 9999)]
        private float _maxHealthPoints = 100f;
        [SerializeField]
        private bool _isEnemy = true;

        private const string dieTrigger = "die";

        private AIController _aiController;

        private float _currentHealthPoints;

        public event Action<float> OnHealthPctChanged = delegate {};
        public event Action DeathEvent;

        private bool _isDead = false;
        public bool IsDead { get { return _isDead; } }

        private void Awake() {
            _currentHealthPoints = _maxHealthPoints;
            _aiController = GetComponent<AIController>();
            if(_aiController == null) {
                _isEnemy = false;
            }
        }

        /// <summary>
        /// Take damage from a fighter, and update health bar UI
        /// </summary>
        /// <param name="damage">Damage to take</param>
        public void TakeDamage(float damage) {
            if(_isEnemy) {
                _aiController.MoveToPlayer();
            }
            _currentHealthPoints = Mathf.Max(_currentHealthPoints - damage, 0);

            float healthPct = _currentHealthPoints / _maxHealthPoints;
            OnHealthPctChanged(healthPct);

            if (_currentHealthPoints <= 0) {
                Die();
            }
        }

        /// <summary>
        /// Capture state to save
        /// </summary>
        /// <returns>State to save</returns>
        public object CaptureState() {
            return _currentHealthPoints;
        }

        /// <summary>
        /// Restore state to load
        /// </summary>
        /// <param name="state">State to load</param>
        public void RestoreState(object state) {
            _currentHealthPoints = (float)state;
            if (_currentHealthPoints <= 0) {
                Die();
            }
        }

        /// <summary>
        /// Behaviour on death
        /// </summary>
        private void Die() {
            if (_isDead) return;
            _isDead = true;
            DeathEvent?.Invoke();
            GetComponent<Animator>().SetTrigger(dieTrigger);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}