using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat {
    /// <summary>
    /// Deals damage on trigger enter.
    /// Attached to a weapon/projectile collidier.
    /// </summary>
    public class DamageDealer : MonoBehaviour {
        [SerializeField]
        private string _playerTag = "Player";

        [SerializeField]
        [Range(0,10)]
        [Tooltip("For how long to be able to deal damage after starting the attack")]
        private float _damageDealerLifetime = 0.5f;

        private Fighter _player;

        private void Start() {
            _player = GameObject.FindWithTag(_playerTag).GetComponent<Fighter>();         
        }

        private void OnTriggerEnter(Collider other) {
            if(_player.TimeSinceLastAttack < _damageDealerLifetime) {
                Health enemyHealth = other.GetComponent<Health>();
                if(enemyHealth != null) {
                    enemyHealth.TakeDamage(_player.Damage);
                }
            }
        }
    }
}
