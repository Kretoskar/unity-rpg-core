using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour {
    [SerializeField] private Transform _target;
    private NavMeshAgent playerNavMeshAgent;

    private void Start() {
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            MoveToCursor();
        }
    }

    private void MoveToCursor() {
        Ray ray = RecalculateRaycast();
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if(hasHit) {
            playerNavMeshAgent.destination = hit.point;
        }
    }

    private Ray RecalculateRaycast() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
