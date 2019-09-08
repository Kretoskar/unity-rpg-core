﻿using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control {
    /// <summary>
    /// Handles player's character interaction
    /// </summary>
    [RequireComponent(typeof(Fighter))]
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Health))]
    public class PlayerController : MonoBehaviour {

        private Fighter _fighter;
        private Mover _mover;
        private Health _health;
        private Joystick _joystick;

        #region Singleton

        private static PlayerController _instance;
        public static PlayerController Instance { get => _instance; set => _instance = value; }

        private void SetupSingleton() {
            if (_instance != null && _instance != this) {
                Destroy(gameObject);
            }
            else {
                _instance = this;
            }
        }

        #endregion

        #region MonoBehaviour Methods

        private void Awake() {
            SetupSingleton();
            _fighter = GetComponent<Fighter>();
            _mover = GetComponent<Mover>();
            _health = GetComponent<Health>();
        }

        private void Start() {
            _joystick = FindObjectOfType<Joystick>();
        }

        private void Update() {
            if (_health.IsDead) return;
            //if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check for player input to handle combat
        /// </summary>
        public void InteractWithCombat() {
            _fighter.PlayerAttack();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check for player input to handle movement
        /// </summary>
        private bool InteractWithMovement() {
            float horizontal = _joystick.Horizontal;
            float vertical = _joystick.Vertical;
            if(Mathf.Abs(horizontal) < Mathf.Epsilon && Mathf.Abs(vertical) < Mathf.Epsilon) {
                _mover.StopPlayer();
                return false;
            } else {
                _mover.MovePlayer(horizontal, vertical);
                return true;
            }
        }

        /// <summary>
        /// Calculatre ray of the mouse click
        /// </summary>
        /// <returns> Ray of the mouse click </returns>
        private Ray GetMouseRay() {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        #endregion
    }
}