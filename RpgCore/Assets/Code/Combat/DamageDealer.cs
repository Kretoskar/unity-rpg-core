using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat {
    public class DamageDealer : MonoBehaviour {
        [SerializeField]
        private string _playerTag = "Player";

        private Fighter _player;

        private void Start() {
            _player = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            
        }

        private void OnTriggerEnter(Collider other) {
            if(_player.TimeSinceLastAttack < 0.5f) {
                Health enemyHealth = other.GetComponent<Health>();
                if(enemyHealth != null) {
                    enemyHealth.TakeDamage(_player.Damage);
                }
            }
        }
    }
}
