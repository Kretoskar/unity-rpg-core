﻿using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control {
    /// <summary>
    /// Handles player's character interaction
    /// </summary>
    public class PlayerController : MonoBehaviour {

        private Fighter _fighter;
        private Mover _mover;
        private Health _health;

        private void Start() {
            _fighter = GetComponent<Fighter>();
            _mover = GetComponent<Mover>();
            _health = GetComponent<Health>();
        }

        private void Update() {
            if (_health.IsDead) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        /// <summary>
        /// Check for player input to handle combat
        /// </summary>
        private bool InteractWithCombat() {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits) {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null) continue;

                if (!_fighter.CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0)) {
                    _fighter.Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check for player input to handle movement
        /// </summary>
        private bool InteractWithMovement() {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit) {
                if (Input.GetMouseButton(0)) {
                    _mover.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Calculatre ray of the mouse click
        /// </summary>
        /// <returns> Ray of the mouse click </returns>
        private Ray GetMouseRay() {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}