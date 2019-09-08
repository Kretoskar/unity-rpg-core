using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


namespace RPG.SceneManagement {
    /// <summary>
    /// Portal that teleports player to a portal on another scene
    /// </summary>
    public class Portal : MonoBehaviour {

        enum DestinationIdentifier {
            A, B, C, D, E
        }

        [SerializeField]
        private Transform _spawnPoint = null;
        [SerializeField]
        private DestinationIdentifier _destination;
        [SerializeField]
        private float _fadeInOutTime = 1f;
        [SerializeField]
        private float _fadeWaitTime = .5f;
        [SerializeField]
        private int _indexOfSceneToLoad = -1;

        /// <summary>
        /// Teleport to another scene on trigger enter
        /// </summary>
        /// <param name="other">collider that came to trigger</param>
        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                StartCoroutine(Transition());
            }
        }

        /// <summary>
        /// Teleport through portals
        /// Takes player from one scene to another
        /// </summary>
        /// <returns>waiting for transitioning between scenes</returns>
        private IEnumerator Transition() {

            // Make sure the scene is loaded
            if(_indexOfSceneToLoad < 0) {
                Debug.LogError("Scene to load not set");
                yield break;
            }

            // Set up
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();

            // Fade out
            yield return fader.FadeOut(_fadeInOutTime);

            //Save the game before transitioning
            savingWrapper.Save();

            // Load Scene
            yield return SceneManager.LoadSceneAsync(_indexOfSceneToLoad);

            //Load the game after transitioning
            savingWrapper.Load();

            // Spawn player
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            //Save after teleporting
            savingWrapper.Save();

            // Wait between fadings to stabilize cam and stuff
            yield return new WaitForSeconds(_fadeWaitTime);

            // Fade in
            yield return fader.FadeIn(_fadeInOutTime);

            // Destroy itself
            Destroy(gameObject);
        }

        /// <summary>
        /// Set up player's position in a place of other portal's spawn point
        /// </summary>
        /// <param name="otherPortal">the portal to teleport to</param>
        private void UpdatePlayer(Portal otherPortal) {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal._spawnPoint.position;
            player.transform.rotation = otherPortal._spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        /// <summary>
        /// Find the portal to teleport to
        /// </summary>
        /// <returns>the portal to teleport to</returns>
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
