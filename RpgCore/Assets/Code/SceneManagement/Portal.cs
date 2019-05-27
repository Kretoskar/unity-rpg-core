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

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition() {
            if(_indexOfSceneToLoad < 0) {
                Debug.LogError("Scene to load not set");
                yield break;
            }

            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(_indexOfSceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

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
