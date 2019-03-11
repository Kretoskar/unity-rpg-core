using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour {
    [SerializeField] private Transform _target;

    private void Start() {
        NavMeshAgent playerNavMeshAgent = GetComponent<NavMeshAgent>();
        playerNavMeshAgent.destination = _target.transform.position;
    }
}
