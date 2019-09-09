using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Control;

namespace RPG.UI {
    public class StatsUI : MonoBehaviour {
        [SerializeField]
        private Text _expText = null;

        [SerializeField]
        private Text _levelText = null;

        [SerializeField]
        private Text _strengthText = null;

        [SerializeField]
        private Text _durabilityText = null;

        [SerializeField]
        private Text _powerText = null;


        private PlayerStats _playerStats;

        private void OnEnable() {
            SetupStatsUI();
        }

        private void Start() {
            _playerStats = PlayerController.Instance.GetComponent<PlayerStats>();
            //SetupStatsUI();
        }

        private void SetupStatsUI() {
            _expText.text = "0/100";
            _levelText.text = _playerStats.Level.ToString();
            _strengthText.text = _playerStats.Strength.ToString();
            _durabilityText.text = _playerStats.Durability.ToString();
            _powerText.text = _playerStats.Power.ToString();
        }
    }
}
