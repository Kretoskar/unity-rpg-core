using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement {
    /// <summary>
    /// Move player with joystick
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMover : MonoBehaviour {

        [SerializeField]
        [Range(0, 50)]
        private float _speed = 5f;

        private Transform _mainCam;

        private Vector3 _camForward;
        private Vector3 _camRight;
        private Vector3 _prevPosition;

        private Joystick _joystick;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private const string _animatorBlendValue = "ForwardSpeed";

        private void Awake() {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Start() {
            _mainCam = Camera.main.transform;
            SetupRelativeToCameraTransform();
            _navMeshAgent.enabled = true;
            _joystick = FindObjectOfType<Joystick>();
        }

        private void Update() {
            UpdateAnimator();
        }

        /// <summary>
        /// Move the player character acording to user's input
        /// </summary>
        public void Move(float horizontal, float vertical) {

            Vector3 velocity = (_camForward * vertical * _speed + _camRight * horizontal * _speed);
            //UpdateAnimator(velocity);
            _navMeshAgent.Move(velocity * Time.deltaTime);

            if (velocity != Vector3.zero) {
                transform.rotation = Quaternion.LookRotation(velocity);
            }
        }

        /// <summary>
        /// Setup global Vector3 variables so that player can move
        /// relative to camera's transform
        /// </summary>
        private void SetupRelativeToCameraTransform() {
            _camForward = _mainCam.forward;
            _camForward.y = 0;
            _camForward = _camForward.normalized;
            _camRight = _mainCam.right;
            _camRight.y = 0;
            _camRight = _camRight.normalized;
        }

        /// <summary>
        /// Update animator that uses blend tree
        /// depending on playerNavMeshAgent velocity z value
        /// </summary>
        private void UpdateAnimator() {
            // Transfer from global to local
            Vector3 curMove = transform.position - _prevPosition;
            float speed = curMove.magnitude / Time.deltaTime;
            GetComponent<Animator>().SetFloat(_animatorBlendValue, speed);
            _prevPosition = transform.position;
        }
    }
}