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
        [SerializeField]
        private GameObject _inventoryUI = null;

        private Image _attackButtonImage;
        private PlayerController _playerController;

        #region Singleton

        private static UIController _instance;
        public static UIController Instance { get => _instance; set => _instance = value; }

        private void SetupSingleton() {
            if (_instance != null && _instance != this) {
                Destroy(gameObject);
            }
            else {
                _instance = this;
            }
        }

        #endregion

        private void Awake() {
            SetupSingleton();
        }

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

        /// <summary>
        /// Show or hide inventory UI
        /// </summary>
        public void HideOrShowInventoryUI() {
            _inventoryUI.SetActive(!_inventoryUI.activeSelf);
        }
    }
}