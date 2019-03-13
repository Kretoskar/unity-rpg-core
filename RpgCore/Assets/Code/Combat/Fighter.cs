﻿using UnityEngine;
using RPG.Movement;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour {

        [SerializeField] private float _weaponRange = 2f;

        private Mover _mover;
        private Transform _target;

        private void Start() {
            _mover = GetComponent<Mover>();
        }

        private void Update() {
            bool isInRange = Vector3.Distance(transform.position, _target.position) < _weaponRange;
            if(_target != null && !isInRange) {
                _mover.MoveTo(_target.position);
            }
            else {
                _mover.Stop();
            }
        }

        public void Attack(CombatTarget combatTarget) {
            _target = combatTarget.transform;
        }
    }
}
