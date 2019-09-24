using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats {
    public static class GlobalStats {
        public static float AttackDamage(float weaponDamage, float strength) {
            return strength * 10 + weaponDamage * 2;
        }

        public static float MagicDamage(float power) {
            return power * 10;
        }

        public static float Health(float durability) {
            return durability * 100;
        }

        public static float Armor(float armor) {
            return 50 + armor;
        }
    }
}
