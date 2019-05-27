using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement {
    public class Fader : MonoBehaviour {

        private CanvasGroup _canvasGroup;

        private void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }

        private IEnumerator FadeOutIn() {
            yield return FadeOut(1f);
            yield return FadeIn(1f);
        }

        public IEnumerator FadeOut(float time) {
            while(_canvasGroup.alpha < 1) {
                _canvasGroup.alpha += Time.deltaTime / time;
                yield return null; // waits one frame
            }
        }

        public IEnumerator FadeIn(float time) {
            while(_canvasGroup.alpha > 0) {
                _canvasGroup.alpha -= Time.deltaTime / time;
                yield return null; // waits one frame
            }
        }
    }
}
