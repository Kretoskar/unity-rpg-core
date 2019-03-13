using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Move character and update his animator
/// </summary>
public class Mover : MonoBehaviour {
    private const string _animatorBlendValue = "ForwardSpeed";

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private void Start() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        UpdateAnimator();
    }

    /// <summary>
    /// Move character to given destination
    /// </summary>
    /// <param name="destination">Where to move the character</param>
    public void MoveTo(Vector3 destination) {
        _navMeshAgent.destination = destination;
    }

    /// <summary>
    /// Update animator that uses blend tree
    /// depending on playerNavMeshAgent velocity z value
    /// </summary>
    private void UpdateAnimator() {
        Vector3 velocity = _navMeshAgent.velocity;
        // Transfer from global to local
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        _animator.SetFloat(_animatorBlendValue, speed);
    }
}