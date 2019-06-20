using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    public class Projectile : MonoBehaviour {

        [SerializeField] private float _speed = 1;
        [SerializeField] private float _howHighToAim = 1.1f;

        private Health _target = null;
        private float _damage = 0;

        private void Update() {
            Shoot();
        }

        public void SetTarget(Health target, float damage) {
            _target = target;
            _damage = damage;
        }

        private void Shoot() {
            if (_target == null)
                return;
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

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
            _target.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
