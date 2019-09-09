using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Stats {
    public class PlayerStats : BaseStats, ISaveable {

        private List<int> _saveableStatsList;

        private int _levelIndex = 0;
        private int _strengthIndex = 1;
        private int _durabilityIndex = 2;
        private int _powerIndex = 3;

        public int Level { get; set; }
        public override int Strength { get => base.Strength; set { base.Strength = value; SaveStats(); } }
        public override int Durability { get => base.Durability; set => base.Durability = value; }
        public override int Power { get => base.Power; set => base.Power = value; }

        private void Awake() {
            _saveableStatsList = new List<int>();
            _saveableStatsList.Add(Level);
            _saveableStatsList.Add(Strength);
            _saveableStatsList.Add(Durability);
            _saveableStatsList.Add(Power);

            Level = 1;
            Strength = 2;
            Durability = 3;
            Power = 4;
        }

        public object CaptureState() {
            return _saveableStatsList;
        }

        public void RestoreState(object state) {
            List<int> stats = (List<int>)state;
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
