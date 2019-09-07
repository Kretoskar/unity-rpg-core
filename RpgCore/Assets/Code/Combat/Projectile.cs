using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    public class Projectile : MonoBehaviour {

        [SerializeField] private float _speed = 1;
        [SerializeField] private float _howHighToAim = 1.1f;
        [SerializeField] private bool _isHoming = false;
        [SerializeField] private float _maxLifeTime = 5;
        [SerializeField] private GameObject _hitEffect = null;

        private Health _target = null;
        private Vector3 _playerForward;
        private float _damage = 0;
        private bool _isShotByPlayer = true;

        private void Start() {
            transform.LookAt(GetAimLocation());
        }

        private void Update() {
            if (!_isShotByPlayer) {
                Shoot();
            } else {
                ShootByPlayer();
            }
        }

        private void ShootByPlayer() {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        /// <summary>
        /// Set projectile' target
        /// </summary>
        /// <param name="target">Target to shoot at</param>
        /// <param name="damage">Damage for the target to take</param>
        public void SetTarget(Health target, float damage) {
            _isShotByPlayer = false;
            _target = target;
            _damage = damage;

            Destroy(gameObject, _maxLifeTime);
        }

        /// <summary>
        /// Shoot projectile by player
        /// </summary>
        /// <param name="damage"></param>
        public void SetTarget(float damage, Vector3 forward) {
            _playerForward = forward;
            _isShotByPlayer = true;
            _damage = damage;

            Destroy(gameObject, _maxLifeTime);
        }

        /// <summary>
        /// Shoot the projectile
        /// </summary>
        private void Shoot() {
            if (_target == null)
                return;
            if(_isHoming && !_target.IsDead)
                transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        /// <summary>
        /// Where to shoot the projectile
        /// </summary>
        /// <returns></returns>
        private Vector3 GetAimLocation() {
            if(_isShotByPlayer) {
                print(_playerForward);
                return _playerForward;
            }

            CapsuleCollider target = _target.GetComponent<CapsuleCollider>();
            if (target == null)
                return target.transform.position;
            return target.transform.position + Vector3.up * target.height / _howHighToAim;
        }

        private void OnTriggerEnter(Collider other) {
            Health hitHealth = other.GetComponent<Health>();
            if(hitHealth == null) {
                HitWall();
                return;
            }
            if (_isShotByPlayer) {
                PlayerDamageDeal(hitHealth);
            } else {
                NPCDamageDeal(hitHealth);
            }
            
        }

        private void HitWall() {
            if (_hitEffect != null) {
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        private void PlayerDamageDeal(Health other) {
            if (other.IsDead) return;
            other.TakeDamage(_damage);

            if (_hitEffect != null) {
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        private void NPCDamageDeal(Health other) {
            if (other.GetComponent<Health>() != _target) {
                return;
            }
            if (_target.IsDead) return;
            _target.TakeDamage(_damage);

            if (_hitEffect != null) {
                Instantiate(_hitEffect, GetAimLocation(), Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
