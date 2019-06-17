using RPG.Saving;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement {
    /// <summary>
    /// In game logic of saving
    /// </summary>
    public class SavingWrapper : MonoBehaviour {
        const string _defaultSaveFile = "save";
        [SerializeField]
        private float _fadeInTime = .5f;

        /// <summary>
        /// Load game on start
        /// </summary>
        /// <returns></returns>
        private IEnumerator Start() {
            Fader fader = FindObjectOfType<Fader>();

            fader.FadeOutImmediately();
            yield return GetComponent<SavingSystem>().LoadLastScene(_defaultSaveFile);
            yield return fader.FadeIn(_fadeInTime);
        }

        /// <summary>
        /// Check for player input
        /// </summary>
        private void Update() {
            if (Input.GetKeyDown(KeyCode.S))
                Save();
            if (Input.GetKeyDown(KeyCode.L))
                Load();
            if (Input.GetKeyDown(KeyCode.E))
                Erase();
        }

        /// <summary>
        /// Load saved game
        /// </summary>
        public void Load() {
            GetComponent<SavingSystem>().Load(_defaultSaveFile);
        }

        /// <summary>
        /// Save the game
        /// </summary>
        public void Save() {
            GetComponent<SavingSystem>().Save(_defaultSaveFile);
        }
        
        /// <summary>
        /// Erase saved game
        /// </summary>
        public void Erase() {
            GetComponent<SavingSystem>().Erase(_defaultSaveFile);
        }
    }
}
