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

        [SerializeField]
        private List<Button> _plusMinusButtons = new List<Button>();

        #region Singleton

        private static StatsUI _instance;
        public static StatsUI Instance { get => _instance; set => _instance = value; }

        private void SetupSingleton() {
            if (_instance != null && _instance != this) {
                Destroy(gameObject);
            }
            else {
                _instance = this;
            }
        }

        #endregion

        private PlayerStats _playerStats;
        private StatPoints _statPoints;

        private void Awake() {
            SetupSingleton();
            gameObject.SetActive(false);
        }

        private void OnEnable() {
            UpdateStats();
        }

        private void Start() {
            _statPoints = StatPoints.Instance;
            _playerStats = PlayerStats.Instance;
            _statPoints = StatPoints.Instance;
            _playerStats.LevelChanged += SetupStatButtons;
            SetupStatsUI();
        }

        public void UpdateStats() {
            SetupStatsUI();
            SetupStatButtons();
        }

        public void ChangeStrength(int value) {
            _statPoints.Points -= value;
            _playerStats.Strength += value;
            UpdateStats();
        }

        public void ChangeDurability(int value) {
            _statPoints.Points -= value;
            _playerStats.Durability += value;
            UpdateStats();
        }

        public void ChangePower(int value) {
            _statPoints.Points -= value;
            _playerStats.Power += value;
            UpdateStats();
        }

        private void SetupStatsUI() {
            if (_playerStats == null) return;
            _expText.text = _playerStats.Exp.ToString() + " / " + _playerStats.Level * 100;
            _levelText.text = _playerStats.Level.ToString();
            _strengthText.text = _playerStats.Strength.ToString();
            _durabilityText.text = _playerStats.Durability.ToString();
            _powerText.text = _playerStats.Power.ToString();
        }

        private void SetupStatButtons() {
            if (_statPoints == null) {
                foreach (Button button in _plusMinusButtons) {
                    button.gameObject.SetActive(false);
                }
                return;
            }
            if(_statPoints.Points > 0) {
                foreach (Button button in _plusMinusButtons) {
                    button.gameObject.SetActive(true);
                }
            } else {
                foreach (Button button in _plusMinusButtons) {
                    button.gameObject.SetActive(false);
                }
            }
            print(_statPoints.Points);
        }
    }
}
