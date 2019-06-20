using UnityEngine;

namespace RPG.Combat {
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make new weapon", order = 0)]
    public class Weapon : ScriptableObject {
        [SerializeField] private GameObject _equippedPrefab = null;
        [SerializeField] private AnimatorOverrideController _animatorOverride = null;
        [SerializeField] private float _weaponDamage = 5f;
        [SerializeField] private float _weaponRange = 4f;
        [SerializeField] private bool _isRightHanded = true;

        /// <summary>
        /// Spawn the weapon
        /// </summary>
        /// <param name="rightHandTransform">transform of player's hand</param>
        /// <param name="animator">player's animator</param>
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator) {
            if (_equippedPrefab != null) { 

                Transform handTransform;
                if (_isRightHanded)
                    handTransform = rightHand;
                else
                    handTransform = leftHand;

                Instantiate(_equippedPrefab, handTransform);
            }
            
            if(_animatorOverride != null)
                animator.runtimeAnimatorController = _animatorOverride;
        }

        public float GetRange() {
            return _weaponRange;
        }

        public float GetDamage() {
            return _weaponDamage;
        }
    }
}
