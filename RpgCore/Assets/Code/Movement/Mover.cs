using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour {
    #region private members

    private const string _animatorBlendValue = "ForwardSpeed";

    private NavMeshAgent _playerNavMeshAgent;
    private Animator _playerAnimator;

    #endregion

    #region monobehaviour methods

    /// <summary>
    /// Get component of the nav mesh agent
    /// </summary>
    private void Start() {
        _playerNavMeshAgent = GetComponent<NavMeshAgent>();
        _playerAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Check for clicks and update animator
    /// </summary>
    private void Update() {
        if(Input.GetMouseButton(0)) {
            MoveToCursor();
        }
        UpdateAnimator();
    }

    #endregion

    #region custom methods

    /// <summary>
    /// Move character to raycast hit position
    /// </summary>
    private void MoveToCursor() {
        Ray ray = RecalculateRaycast();
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if(hasHit) {
            _playerNavMeshAgent.destination = hit.point;
        }
    }

    /// <summary>
    /// Calculatre ray of the mouse click
    /// </summary>
    /// <returns> Ray of the mouse click </returns>
    private Ray RecalculateRaycast() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    /// <summary>
    /// Update animator that uses blend tree
    /// depending on playerNavMeshAgent velocity z value
    /// </summary>
    private void UpdateAnimator() {
        Vector3 velocity = _playerNavMeshAgent.velocity;
        // Transfer from global to local
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        _playerAnimator.SetFloat(_animatorBlendValue, speed);
    }

    #endregion

}