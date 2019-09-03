using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Control;

namespace RPG.UI {
    public class UIController : MonoBehaviour {
        [SerializeField]
        private Button _attackButton = null;

        private PlayerController _playerController;

        private void Start() {
            _playerController = FindObjectOfType<PlayerController>();
            _attackButton.onClick.AddListener(_playerController.InteractWithCombat);
        }

    }
}