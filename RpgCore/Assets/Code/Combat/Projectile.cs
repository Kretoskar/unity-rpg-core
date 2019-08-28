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
        private float _damage = 0;

        private void Start() {
            transform.LookAt(GetAimLocation());
        }

        private void Update() {
            Shoot();
        }

        /// <summary>
        /// Set projectile' target
        /// </summary>
        /// <param name="target">Target to shoot at</param>
        /// <param name="damage">Damage for the target to take</param>
        public void SetTarget(Health target, float damage) {
            _target = target;
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
        /// How high should the projectile hit
        /// </summary>
        /// <returns></returns>
        private Vector3 GetAimLocation() {
            CapsuleCollider target = _target.GetComponent<CapsuleCollider>();
            if (target == null)
                return target.transform.position;
            return target.transform.position + Vector3.up * target.height / _howHighToAim;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.GetComponent<Health>() != _target) {
                return;
            }
            if (_target.IsDead) return;
            _target.TakeDamage(_damage);

            if(_hitEffect != null) {
                 Instantiate(_hitEffect, GetAimLocation(), Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
