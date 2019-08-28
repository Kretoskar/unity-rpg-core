using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core {
    public class DestroyAfterEffect : MonoBehaviour {

        private ParticleSystem _particle;

        private void Awake() {
            _particle = GetComponent<ParticleSystem>();
        }

        private void Update() {
            if(!_particle.IsAlive()) {
                if (transform.parent != null) {
                    Destroy(transform.parent.gameObject);
                } else {
                    Destroy(gameObject);
                }
            }
        }
    }
}
