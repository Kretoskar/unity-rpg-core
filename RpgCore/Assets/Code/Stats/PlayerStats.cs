using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.UI;

namespace RPG.Stats {
    public class PlayerStats : BaseStats, ISaveable {

        #region Singleton

        private static PlayerStats _instance;
        public static PlayerStats Instance { get => _instance; set => _instance = value; }

        private void SetupSingleton() {
            if (_instance != null && _instance != this) {
                Destroy(gameObject);
            }
            else {
                _instance = this;
            }
        }

        #endregion

        private int _levelIndex = 0;
        private int _strengthIndex = 1;
        private int _durabilityIndex = 2;
        private int _powerIndex = 3;

        private List<int> _saveableStatsList;

        private StatsUI _statsUI;

        public event Action LevelChanged;

        private int _level;
        public int Level {
            get {
                return _level;
            }
            set {
                if (StatPoints.Instance != null)
                    StatPoints.Instance.Points += (value - _level) * 3;
                _level = value;
                LevelChanged?.Invoke();
            }
        }

        private int _exp;
        public int Exp {
            get {
                return _exp;
            }
            set {
                _exp = value;
                if(_statsUI != null)
                    _statsUI.UpdateStats();
                if(Exp > Level * 100) {
                    Level++;
                }
            }
        }
        public override int Strength { get => base.Strength; set { base.Strength = value; } }
        public override int Durability { get => base.Durability; set => base.Durability = value; }
        public override int Power { get => base.Power; set => base.Power = value; }

        private void Awake() {
            SetupSingleton();

            Level = 1;
            Exp = 0;
            Strength = 1;
            Durability = 1;
            Power = 1;

            _saveableStatsList = new List<int>();
            _saveableStatsList.Add(Level);
            _saveableStatsList.Add(Strength);
            _saveableStatsList.Add(Durability);
            _saveableStatsList.Add(Power);
        }

        private void Start() {
            print("start");
            _statsUI = StatsUI.Instance;
        }

        public void DummyAddExp() {
            Exp += 50;
        }

        public object CaptureState() {
            return _saveableStatsList;
        }

        public void RestoreState(object state) {
            List<int> stats = (List<int>)state;
            if (stats == null)
                return;
            Strength = stats[_strengthIndex];
            Durability = stats[_durabilityIndex];
            Power = stats[_powerIndex];
        }

        private void SaveStats() {
            _saveableStatsList[_levelIndex] = Level;
            _saveableStatsList[_strengthIndex] = Strength;
            _saveableStatsList[_durabilityIndex] = Durability;
            _saveableStatsList[_powerIndex] = Power;
            CaptureState();
        }
    }
}
