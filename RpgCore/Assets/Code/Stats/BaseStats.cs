using UnityEngine;

namespace RPG.Stats {
    /// <summary>
    /// Characters stats
    /// </summary>
    public class BaseStats : MonoBehaviour {
        public virtual int Strength { get; set; }
        public virtual int Durability { get; set; }
        public virtual int Power { get; set; }
    }
}
