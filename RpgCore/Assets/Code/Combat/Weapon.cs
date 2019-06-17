using UnityEngine;

namespace RPG.Combat {
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make new weapon", order = 0)]
    public class Weapon : ScriptableObject {
        [SerializeField] private GameObject _weaponPrefab = null;
        [SerializeField] private AnimatorOverrideController _animatorOverride = null;

        /// <summary>
        /// Spawn the weapon
        /// </summary>
        /// <param name="handTransform">transform of player's hand</param>
        /// <param name="animator">player's animator</param>
        public void Spawn(Transform handTransform, Animator animator) {
            Instantiate(_weaponPrefab, handTransform);
            animator.runtimeAnimatorController = _animatorOverride;
        }
    }
}
