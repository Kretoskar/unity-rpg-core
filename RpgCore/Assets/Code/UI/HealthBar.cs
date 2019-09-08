using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    public class HealthBar : MonoBehaviour {
        [SerializeField]
        private Image _foregroundImage = null;
        [SerializeField]
        private float _updateSpeedSeconds = 1f;

        private Camera _mainCamera;

        private void Awake() {
            _mainCamera = Camera.main;
            GetComponentInParent<Health>().OnHealthPctChanged += HandleHealthChanged;
        }

        private void HandleHealthChanged(float pct) {
            StartCoroutine(ChangeToPct(pct));
        }

        private IEnumerator ChangeToPct(float pct) {
            float preChangePct = _foregroundImage.fillAmount;
            float elapsed = 0f;

            while (elapsed < _updateSpeedSeconds) {
                elapsed += Time.deltaTime;
                _foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / _updateSpeedSeconds);
                yield return null;
            }

            _foregroundImage.fillAmount = pct;
        }

        private void LateUpdate() {
            transform.LookAt(_mainCamera.transform);
            //transform.Rotate(0, 180, 0);
        }
    }
}
