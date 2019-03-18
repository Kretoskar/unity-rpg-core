using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control {
    public class AIController : MonoBehaviour {
        [SerializeField] private float _chaseDistance = 5f;

        private GameObject _player;

        private void Start() {
            _player = GameObject.FindWithTag("Player");
        }

        private void Update() {
            if(DistanceToPlayer() < _chaseDistance) {
                print("Should chase");
            }
        }

        private float DistanceToPlayer() {
            return Vector3.Distance(_player.transform.position, transform.position);
        }
    }
}
