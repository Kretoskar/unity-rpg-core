using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats {
    /// <summary>
    /// Player stats data
    /// </summary>
    public class PlayerStats : MonoBehaviour {
        [SerializeField]
        private int _expToNextLevel = 1000;

        private int _strength;
        private int _durability;
        private int _power;
        private int _exp;
        private int _level;

        public int Strength { get => _strength; set => _strength = value; }
        public int Durability { get => _durability; set => _durability = value; }
        public int Power { get => _power; set => _power = value; }
        public int Exp { get => _exp; set => _exp = value; }
        public int Level { get => _exp / _expToNextLevel; private set => _level = value; }
    }
}
