using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace RPG.SceneManagement {
    public class Portal : MonoBehaviour {

        enum DestinationIdentifier {
            A, B, C, D, E
        }

        [SerializeField]
        private int _indexOfSceneToLoad = -1;
        [SerializeField]
        private Transform _spawnPoint = null;
        [SerializeField]
        private DestinationIdentifier _destination;
        [SerializeField]
        private float _fadeInOutTime = 1f;
        [SerializeField]
        private float _fadeWaitTime = .5f;

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition() {

            // Make sure the scene is loaded
            if(_indexOfSceneToLoad < 0) {
                Debug.LogError("Scene to load not set");
                yield break;
            }

            // Set up
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();

            // Fade out
            yield return fader.FadeOut(_fadeInOutTime);

            // Load Scene
            yield return SceneManager.LoadSceneAsync(_indexOfSceneToLoad);

            // Spawn player
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            // Wait between fadings to stabilize cam and stuff
            yield return new WaitForSeconds(_fadeWaitTime);

            // Fade in
            yield return fader.FadeIn(_fadeInOutTime);

            // Destroy itself
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal) {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = otherPortal._spawnPoint.position;
            player.transform.rotation = otherPortal._spawnPoint.rotation;
        }

        private Portal GetOtherPortal() {
            foreach (Portal portal in FindObjectsOfType<Portal>()) {
                if (portal == this)
                    continue;
                if (portal._destination != _destination)
                    continue;
                return portal;
            }
            return null;
        }
    }
}
