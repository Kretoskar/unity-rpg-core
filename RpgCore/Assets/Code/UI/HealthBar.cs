using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    /// <summary>
    /// Displays health bar and handles changing it's fill amount
    /// </summary>
    public class HealthBar : MonoBehaviour {
        [SerializeField]
        private Image _foregroundImage = null;
        [SerializeField]
        private float _updateSpeedSeconds = 1f;

        private Camera _mainCamera;

        private void Awake() {
            _mainCamera = Camera.main;
            Health health = GetComponentInParent<Health>();
            health.OnHealthPctChanged += HandleHealthChanged;
            health.DeathEvent += Death;
        }

        private void LateUpdate() {
            transform.LookAt(_mainCamera.transform);
            //transform.Rotate(0, 180, 0);
        }

        /// <summary>
        /// Start changing healthbar fill amount when health changes
        /// </summary>
        /// <param name="pct"></param>
        private void HandleHealthChanged(float pct) {
            StartCoroutine(ChangeToPct(pct));
        }

        /// <summary>
        /// Change healthbar fillamount
        /// </summary>
        /// <param name="pct">pct of health</param>
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

        /// <summary>
        /// Destroy health bar on death
        /// </summary>
        private void Death() {
            Destroy(gameObject);
        }
    }
}
