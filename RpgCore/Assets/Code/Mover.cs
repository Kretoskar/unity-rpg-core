using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour {
    [SerializeField] private Transform _target;

    private Ray _lastRay;

    private void Start() {
        NavMeshAgent playerNavMeshAgent = GetComponent<NavMeshAgent>();
        playerNavMeshAgent.destination = _target.transform.position;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            _lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        Debug.DrawRay(_lastRay.origin, _lastRay.direction * 100);
    }
}
