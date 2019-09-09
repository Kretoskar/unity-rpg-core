using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Control;
using RPG.Combat;

namespace RPG.UI {
    /// <summary>
    /// Set attack button fill amount
    /// </summary>
    public class UIController : MonoBehaviour {
        [SerializeField]
        private Button _attackButton = null;

        [SerializeField]
        private GameObject _statsUI = null;

        private Image _attackButtonImage;
        private PlayerController _playerController;

        private void Start() {
            _attackButtonImage = _attackButton.GetComponent<Image>();
            _playerController = PlayerController.Instance;
            _attackButton.onClick.AddListener(_playerController.InteractWithCombat);
            _statsUI.SetActive(false);
        }

        private void Update() {
            _attackButtonImage.fillAmount = _playerController.GetAttackButtonFillAmount();
        }

        /// <summary>
        /// Show or hide stats UI
        /// </summary>
        public void HideOrShowStatsUI() {
            _statsUI.SetActive(!_statsUI.activeSelf);
        }

    }
}