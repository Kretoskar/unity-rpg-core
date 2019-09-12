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

        [SerializeField]
        private int _startingExpForNextLevel = 100;
        [SerializeField]
        private float _lvlModifier = 1.1f;
        [SerializeField]
        private int _statPointsForLevel = 3;

        //Indexes for the saveableStatsList to save data
        private int _levelIndex = 0;
        private int _strengthIndex = 1;
        private int _durabilityIndex = 2;
        private int _powerIndex = 3;
        private int _level;
        private int _exp;

        private List<int> _saveableStatsList;
        private StatsUI _statsUI;

        public event Action LevelChanged;
        public event Action StrengthChanged;
        public event Action DurabilityChanged;
        public event Action PowerChanged;

        /// <summary>
        /// Variable to multiply exp to next level  by
        /// </summary>
        public float LevelModifier { get { return _lvlModifier; } }

        /// <summary>
        /// How much exp to get to level up first time.
        /// </summary>
        public int StartingExpForNextLevel { get { return _startingExpForNextLevel; } }

        /// <summary>
        /// Current player level
        /// </summary>
        public int Level {
            get {
                return _level;
            }
            set {
                if (StatPoints.Instance != null)
                    StatPoints.Instance.Points += (value - _level) * _statPointsForLevel;
                _level = value;
                LevelChanged?.Invoke();
            }
        }

        /// <summary>
        /// Current player exp
        /// </summary>
        public int Exp {
            get {
                return _exp;
            }
            set {
                _exp = value;
                if(_statsUI != null)
                    _statsUI.UpdateStats();
                if(Exp >= _startingExpForNextLevel * _lvlModifier * Level) {
                    Level++;
                }
            }
        }

        public override int Strength {   get => base.Strength;   set { base.Strength = value;   StrengthChanged?.Invoke(); }  }
        public override int Durability { get => base.Durability; set { base.Durability = value; DurabilityChanged?.Invoke(); } }
        public override int Power {      get => base.Power;      set { base.Power = value;      PowerChanged?.Invoke(); } }

        private void Awake() {
            SetupSingleton();

            //Fill with dummy data
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
            _statsUI = StatsUI.Instance;
        }

        public object CaptureState() {
            _saveableStatsList[_levelIndex] = Level;
            _saveableStatsList[_strengthIndex] = Strength;
            _saveableStatsList[_durabilityIndex] = Durability;
            _saveableStatsList[_powerIndex] = Power;
            return _saveableStatsList;
        }

        public void RestoreState(object state) {
            List<int> stats = (List<int>)state;
            if (stats == null)
                return;
            Level = stats[_levelIndex];
            Strength = stats[_strengthIndex];
            Durability = stats[_durabilityIndex];
            Power = stats[_powerIndex];
        }
    }
}
