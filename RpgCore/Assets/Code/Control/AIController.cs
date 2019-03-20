﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control {
    public class AIController : MonoBehaviour {
        [SerializeField] private float _chaseDistance = 5f;
        [SerializeField] private float _suspicionTime = 3f;
        [SerializeField] private PatrolPath _patrolPath = null;
        [SerializeField] private float _waypointTolerance = 1f;
        [SerializeField] private float _waypointDwellTime = 3f;

        private GameObject _player;
        private Fighter _fighter;
        private Mover _mover;
        private Health _health;

        private Vector3 _guardPosition;
        private float _timeSincePlayerLastSaw = Mathf.Infinity;
        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int _currentWaypointIndex = 0;

        private void Start() {
            _player = GameObject.FindWithTag("Player");

            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _mover = GetComponent<Mover>();

            _guardPosition = transform.position;
        }

        private void Update() {
            if (_health.IsDead) return;
            // Attack
            if (InAttackRangeOfPlayer() && _fighter.CanAttack(_player)) {
                AttackBehaviour();
            }
            // Suspicion
            else if (_timeSincePlayerLastSaw < _suspicionTime) {
                SuspicionBehaviour();
            }
            // Guard
            else {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        /// <summary>
        /// Update timers for the time since last saw player
        /// and time since arrived at waypoint
        /// </summary>
        private void UpdateTimers() {
            _timeSincePlayerLastSaw += Time.deltaTime;
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        /// <summary>
        /// Behaviour for attacking the player
        /// </summary>
        private void AttackBehaviour() {
            _timeSincePlayerLastSaw = 0;
            _fighter.Attack(_player);
        }

        /// <summary>
        /// Behaviour after losing the player 
        /// </summary>
        private void SuspicionBehaviour() {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        /// <summary>
        /// Behaviour seconds after losing the player
        /// </summary>
        private void PatrolBehaviour() {
            Vector3 nextPosition = _guardPosition;

            if(_patrolPath != null) {
                if (AtWaypoint()) {
                    _timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoints();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if(_timeSinceArrivedAtWaypoint > _waypointDwellTime) {
                _mover.StartMoveAction(nextPosition);
            }
        }

        /// <summary>
        /// Get the position of current waypoint
        /// </summary>
        /// <returns>Position of current waypoint</returns>
        private Vector3 GetCurrentWaypoint() {
            return _patrolPath.GetWaypoint(_currentWaypointIndex);
        }

        /// <summary>
        /// Set the current waypoint index to the next waypoint index
        /// </summary>
        private void CycleWaypoints() {
            _currentWaypointIndex = _patrolPath.GetNextIndex(_currentWaypointIndex);
        }

        /// <summary>
        /// Check if player is near the current waypoint
        /// </summary>
        /// <returns>Is player near current waypoint</returns>
        private bool AtWaypoint() {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < _waypointTolerance;
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
