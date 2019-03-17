using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
    /// <summary>
    /// Handles fight action
    /// </summary>
    public class Fighter : MonoBehaviour, IAction {

        [SerializeField] private float _weaponRange = 2f;
        [SerializeField] private float _timeBetweenAttacks = 0.5f;
        [SerializeField] private float _weaponDamage = 5f;

        private const string triggerName = "attack";
        private const string stopTriggerName = "stopAttack";

        private Health _target;
        private float _timeSinceLastAttack = 0;

        private void Update() {
            _timeSinceLastAttack += Time.deltaTime;

            if (_target == null) return;
            if (_target.IsDead) return;

            //If enemy is not in range, move to him
            if (!GetIsInRange()) {
                GetComponent<Mover>().MoveTo(_target.transform.position);
            }
            else {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        /// <summary>
        /// All of the behaviour for the attack
        /// </summary>
        private void AttackBehaviour() {
            if (_timeSinceLastAttack > _timeBetweenAttacks && _target != null) {
                //This will trigget the Hit() event
                GetComponent<Animator>().SetTrigger(triggerName);
                _timeSinceLastAttack = 0;
            }
        }

        /// <summary>
        /// Animation event
        /// </summary>
        private void Hit() { 
            if(_target != null)
                _target.TakeDamage(_weaponDamage);
        }

        /// <summary>
        /// Returns true if we crossed the weaponRange
        /// </summary>
        /// <returns></returns>
        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, _target.transform.position) < _weaponRange;
        }

        /// <summary>
        /// Set the _target 
        /// </summary>
        /// <param name="combatTarget">Target of the attack</param>
        public void Attack(CombatTarget combatTarget) {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        /// <summary>
        /// Cancel the attack action
        /// </summary>
        public void Cancel() {
            print("Stopped attacking");
            GetComponent<Animator>().SetTrigger(stopTriggerName);
            _target = null;
        }
    }
}
