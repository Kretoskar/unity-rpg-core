using RPG.Saving;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement {
    public class SavingWrapper : MonoBehaviour {
        const string _defaultSaveFile = "save";
        [SerializeField]
        private float _fadeInTime = .5f;

        private IEnumerator Start() {
            Fader fader = FindObjectOfType<Fader>();

            fader.FadeOutImmediately();
            yield return GetComponent<SavingSystem>().LoadLastScene(_defaultSaveFile);
            yield return fader.FadeIn(_fadeInTime);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.S))
                Save();
            if (Input.GetKeyDown(KeyCode.L))
                Load();
        }

        public void Load() {
            GetComponent<SavingSystem>().Load(_defaultSaveFile);
        }

        public void Save() {
            GetComponent<SavingSystem>().Save(_defaultSaveFile);
        }
    }
}
