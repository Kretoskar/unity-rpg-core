using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control {
    public class AIController : MonoBehaviour {
        [SerializeField] private float _chaseDistance = 5f;

        private GameObject _player;
        private Fighter _fighter;
        private Health _health;

        private void Start() {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
        }

        private void Update() {
            if (_health.IsDead) return;
            if(InAttackRangeOfPlayer() && _fighter.CanAttack(_player)) {
                _fighter.Attack(_player);
            } else {
                _fighter.Cancel();
            }
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
