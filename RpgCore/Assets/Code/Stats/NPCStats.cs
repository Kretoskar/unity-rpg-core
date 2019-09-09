using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats {
    public class NPCStats : BaseStats {
        [SerializeField]
        [Range(0,999)]
        private int _startingStrength;

        [SerializeField]
        [Range(0, 999)]
        private int _startingDurability;

        [SerializeField]
        [Range(0, 999)]
        private int _startingPower;

        private void Awake() {
            Strength = _startingStrength;
            Durability = _startingDurability;
            Power = _startingPower;
        }
    }
}
