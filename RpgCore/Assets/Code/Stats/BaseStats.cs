using UnityEngine;

namespace RPG.Stats {
    /// <summary>
    /// Character stats
    /// </summary>
    public class BaseStats : MonoBehaviour {
        private int _strength;
        private int _durability;
        private int _power;

        /// <summary>
        /// Increases character damage
        /// </summary>
        public virtual int Strength {
            get => _strength;
            set {
                _strength = value;
                if(_strength < 1) {
                    _strength = 1;
                }
            }
        }

        /// <summary>
        /// Increases character health points
        /// </summary>
        public virtual int Durability {
            get => _durability;
            set {
                _durability = value;
                if(_durability < 1) {
                    _durability = 1;
                }
            }
        }

        /// <summary>
        /// Increases character spell power
        /// </summary>
        public virtual int Power {
            get => _power;
            set {
                _power = value;
                if(_power < 1) {
                    _power = 1;
                }
            }
        }
    }
}
