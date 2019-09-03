﻿using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;
using RPG.Saving;

namespace RPG.Combat {
    /// <summary>
    /// Handles fight action
    /// </summary>
    public class Fighter : MonoBehaviour, IAction, ISaveable {
        [SerializeField] private float _timeBetweenAttacks = 0.5f;
        [SerializeField] private Transform _rightHandTransform = null;
        [SerializeField] private Transform _leftHandTransform = null;
        [SerializeField] private Transform _forwardProjectileTarget = null;
        [SerializeField] private Weapon _defaultWeapon = null;

        private const string triggerName = "attack";
        private const string stopTriggerName = "stopAttack";

        private Health _target;
        private Mover _mover;
        private Animator _animator;
        private ActionScheduler _actionScheduler;
        private Weapon _currentWeapon;

        private float _timeSinceLastAttack = Mathf.Infinity;
        private bool _isPlayer = false;

        public bool IsAttacking { get; private set; }
        public float TimeSinceLastAttack { get { return _timeSinceLastAttack; } }

        public float Damage {
            get {
                Weapon currentWeapon = _currentWeapon;
                if (currentWeapon == null)
                    return 0;
                return _currentWeapon.GetDamage();
            } }

        private void Awake() {
            IsAttacking = false;
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();

            if (_currentWeapon == null) {
                EquipWeapon(_defaultWeapon);
            }
        }

        private void Update() {
            // Start the attack cooldown timer
            _timeSinceLastAttack += Time.deltaTime;

            if (_target == null) return;
            if (_target.IsDead) return;

            // If enemy is not in range, move to him
            if (!GetIsInRange()) {
                _mover.MoveTo(_target.transform.position);
            }
            // If enemy is in range, attack him
            else {
                _mover.Cancel();
                AttackBehaviour();
            }
        }
        /// <summary>
        /// Checks if character can attack the given combat target
        /// </summary>
        /// <param name="combatTarget">combat target to check</param>
        /// <returns>posibillity of attacking the combat target</returns>
        public bool CanAttack(GameObject combatTarget) {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead;
        }

        /// <summary>
        /// Set the _target for the attack
        /// </summary>
        /// <param name="combatTarget">Target of the attack</param>
        public void Attack(GameObject combatTarget) {
            _actionScheduler.StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        /// <summary>
        /// Cancel the attack action
        /// </summary>
        public void Cancel() {
            StopAttackTrigger();
            _target = null;
        }

        public void PlayerAttack() {
            _isPlayer = true;
            if(_timeSinceLastAttack > _timeBetweenAttacks) {
                IsAttacking = true;
                TriggerAttack();
                _timeSinceLastAttack = 0;
            }
        }

        /// <summary>
        /// Equip player's weapon
        /// </summary>
        public void EquipWeapon(Weapon weapon) {
            _currentWeapon = weapon;
            weapon.Spawn(_rightHandTransform,_leftHandTransform, _animator);
        }

        /// <summary>
        /// What to do when the character attack
        /// </summary>
        private void AttackBehaviour() {
            // Look at the enemy
            transform.LookAt(_target.transform);
            // If cooldown lets us attack, attack
            if (_timeSinceLastAttack > _timeBetweenAttacks && _target != null) {
                // This will trigget the Hit() event
                TriggerAttack();
                // Reset the cooldown timer
                _timeSinceLastAttack = 0;
            }
        }

        /// <summary>
        /// Trigger animator triggers for the attack
        /// </summary>
        private void TriggerAttack() {
            _animator.ResetTrigger(stopTriggerName);
            _animator.SetTrigger(triggerName);
        }

        /// <summary>
        /// Animation event triggered on hit
        /// </summary>
        private void Hit() {
            if (_isPlayer) {
                PlayerHit();
            } else {
                NPCHit();
            }
        }

        /// <summary>
        /// Player's behaviour on animation hit event
        /// shoots projectile, or deals damage
        /// </summary>
        private void PlayerHit() {
            if (_currentWeapon.HasProjectile()) {
                _currentWeapon.LaunchProjectile(_rightHandTransform, _leftHandTransform, _forwardProjectileTarget.position);
            }
            else {
                IsAttacking = false;
            }
        }

        /// <summary>
        /// NPC behaviour on animation hit event
        /// shoots projectile, or deals damage
        /// </summary>
        private void NPCHit() {
            if (_target == null && _isPlayer == false)
                return;

            if (_currentWeapon.HasProjectile()) {
                _currentWeapon.LaunchProjectile(_rightHandTransform, _leftHandTransform, _target);
            }
            else {
                _target.TakeDamage(_currentWeapon.GetDamage());
            }
        }

        /// <summary>
        /// Animation event triggered on shooting
        /// </summary>
        private void Shoot() {
            Hit();
        }

        /// <summary>
        /// Returns true if we crossed the weaponRange
        /// </summary>
        /// <returns></returns>
        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, _target.transform.position) < _currentWeapon.GetRange();
        }

        /// <summary>
        /// Set animator triggers to stop the attack
        /// </summary>
        private void StopAttackTrigger() {
            _animator.ResetTrigger(triggerName);
            _animator.SetTrigger(stopTriggerName);
        }

        public object CaptureState() {
            return _currentWeapon.name;
        }

        public void RestoreState(object state) {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}
