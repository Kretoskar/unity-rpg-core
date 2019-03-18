using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control {
    public class AIController : MonoBehaviour {
        [SerializeField] private float _chaseDistance = 5f;
        [SerializeField] private float _suspicionTime = 3f;

        private GameObject _player;
        private Fighter _fighter;
        private Mover _mover;
        private Health _health;

        private Vector3 _guardPosition;
        private float _timeSincePlayerLastSaw = Mathf.Infinity;

        private void Start() {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _mover = GetComponent<Mover>();

            _guardPosition = transform.position;
        }

        private void Update() {
            if (_health.IsDead) return;
            // Attack
            if(InAttackRangeOfPlayer() && _fighter.CanAttack(_player)) {
                _timeSincePlayerLastSaw = 0;
                AttackBehaviour();
            }
            // Suspicion
            else if (_timeSincePlayerLastSaw < _suspicionTime) {
                SuspicionBehaviour();
            }
            // Guard
            else {
                GuardBehaviour();
            }
            // Update suspicion timer
            _timeSincePlayerLastSaw += Time.deltaTime;
        }

        /// <summary>
        /// Behaviour for attacking the player
        /// </summary>
        private void AttackBehaviour() {
            _fighter.Attack(_player);
        }

        /// <summary>
        /// Behaviour after losing the player 
        /// </summary>
        private void SuspicionBehaviour() {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        /// <summary>
        /// Behaviour seconds after losing the player
        /// </summary>
        private void GuardBehaviour() {
            _mover.StartMoveAction(_guardPosition);
        }

        /// <summary>
        /// Cheks if the player is in the range of chase distance
        /// </summary>
        /// <returns></returns>
        private bool InAttackRangeOfPlayer() {
            float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
            return distanceToPlayer < _chaseDistance;
        }

        // Called by Unity
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
    }
}
