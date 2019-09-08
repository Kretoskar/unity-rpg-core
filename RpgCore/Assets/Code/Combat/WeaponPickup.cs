using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    /// <summary>
    /// Pickup a weapon on trigger enter
    /// </summary>
    public class WeaponPickup : MonoBehaviour {
        [SerializeField]
        [Tooltip("Weapon to pick up")]
        Weapon weapon = null;

        /// <summary>
        /// Pick up weapon on trigger enter
        /// </summary>
        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag == "Player") {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}
