using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats {
    /// <summary>
    /// Stats of non player character
    /// </summary>
    public class NPCStats : BaseStats {
        [SerializeField]
        [Range(1,999)]
        private int _startingStrength;

        [SerializeField]
        [Range(1, 999)]
        private int _startingDurability;

        [SerializeField]
        [Range(1, 999)]
        private int _startingPower;

        private void Awake() {
            Strength = _startingStrength;
            Durability = _startingDurability;
            Power = _startingPower;
        }
    }
}
