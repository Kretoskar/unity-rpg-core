using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
    /// <summary>
    /// Handles fight action
    /// </summary>
    public class Fighter : MonoBehaviour, IAction {

        [SerializeField] private float _weaponRange = 2f;

        private const string triggerName = "attack";

        private Mover _mover;
        private Transform _target;

        private void Start() {
            _mover = GetComponent<Mover>();
        }

        private void Update() {

            if (_target == null) return;

            if (!GetIsInRange()) {
                _mover.MoveTo(_target.position);
            }
            else {
                _mover.Cancel();
                AttackBehaviour();
            }
        }

        /// <summary>
        /// All of the behaviour for the attack
        /// </summary>
        private void AttackBehaviour() {
            GetComponent<Animator>().SetTrigger(triggerName);
        }

        /// <summary>
        /// Returns true if we crossed the weaponRange
        /// </summary>
        /// <returns></returns>
        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, _target.position) < _weaponRange;
        }

        /// <summary>
        /// Start the attack action
        /// </summary>
        /// <param name="combatTarget">Target of the attack</param>
        public void Attack(CombatTarget combatTarget) {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.transform;
        }

        /// <summary>
        /// Cancel the attack action
        /// </summary>
        public void Cancel() {
            _target = null;
        }

        /// <summary>
        /// Animation event
        /// </summary>
        private void Hit() {

        }
    }
}
