using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement {
    /// <summary>
    /// Handles fading on loading scenes
    /// </summary>
    public class Fader : MonoBehaviour {

        private CanvasGroup _canvasGroup;

        private void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }

        public void FadeOutImmediately() {
            _canvasGroup.alpha = 1;
        }

        /// <summary>
        /// Darken the screen
        /// </summary>
        /// <param name="time">time of fading</param>
        /// <returns>waiting for screen to fade</returns>
        public IEnumerator FadeOut(float time) {
            while(_canvasGroup.alpha < 1) {
                _canvasGroup.alpha += Time.deltaTime / time;
                yield return null; // waits one frame
            }
        }

        /// <summary>
        /// Lighten the screen
        /// </summary>
        /// <param name="time">time of fading</param>
        /// <returns>waiting for screen to fade</returns>
        public IEnumerator FadeIn(float time) {
            while(_canvasGroup.alpha > 0) {
                _canvasGroup.alpha -= Time.deltaTime / time;
                yield return null; // waits one frame
            }
        }
    }
}
