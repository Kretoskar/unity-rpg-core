using System;
using RPG.Control;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat {
    /// <summary>
    /// Scriptable object of a weapon
    /// </summary>
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make new weapon", order = 0)]
    public class Weapon : Item {
        [SerializeField]
        private GameObject _equippedPrefab = null;

        [SerializeField]
        private AnimatorOverrideController _animatorOverride = null;

        [SerializeField]
        private Projectile _projectile = null;

        [SerializeField]
        private string _name = "Weapon";

        [SerializeField]
        private string _slug = "weapon";

        [SerializeField]
        [Multiline]
        private string _description = "This is a weapon.";

        [SerializeField]
        [Range(0,9999)]
        private float _damage = 5f;

        [SerializeField]
        [Range(0,100)]
        private float _range = 4f;

        [SerializeField]
        [Range(0, 9999)]
        private int _value = 100;

        [SerializeField]
        private int _rarity = 1;

        [SerializeField]
        private bool _isRightHanded = true;

        [SerializeField]
        private bool _stackable = false;

        [SerializeField]
        private string _ID = System.Guid.NewGuid().ToString();

        private const string _weaponName = "Weapon";

        #region Public Methods

        /// <summary>
        /// Get weapon's Guid
        /// </summary>
        /// <returns>Weapon's guid</returns>
        public override string ID() {
            return _ID;
        }

        /// <summary>
        /// Get weeapon's name
        /// </summary>
        /// <returns>Weapon's name</returns>
        public override string Name() {
            return _name;
        }

        /// <summary>
        /// Get weeapon's value
        /// </summary>
        /// <returns>Weapon's value</returns>
        public override int Value() {
            return _value;
        }

        /// <summary>
        /// Get weeapon's slug
        /// </summary>
        /// <returns>Weapon's slug</returns>
        public override string Slug() {
            return _slug;
        }

        /// <summary>
        /// Get weeapon's description
        /// </summary>
        /// <returns>Weapon's description</returns>
        public override string Description() {
            return _description;
        }

        /// <summary>
        /// Get weapon's rarity
        /// </summary>
        /// <returns>Weapon's rarity</returns>
        public override int Rarity() {
            return _rarity;
        }

        /// <summary>
        /// Get weapon's stackability
        /// </summary>
        /// <returns>True if the weapon is stackable</returns>
        public override bool Stackable() {
            return _stackable;
        }

        /// <summary>
        /// Spawn the weapon
        /// </summary>
        /// <param name="rightHandTransform">transform of player's hand</param>
        /// <param name="animator">player's animator</param>
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator) {
            DestroyOldWeapon(rightHand, leftHand);

            if (_equippedPrefab != null) {
                Transform handTransform = GetTransform(rightHand, leftHand);

                GameObject weapon = Instantiate(_equippedPrefab, handTransform);
                weapon.name = _weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;

            if (_animatorOverride != null) {
                animator.runtimeAnimatorController = _animatorOverride;
            }
            else if (overrideController != null) {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        /// <summary>
        /// Check if weapon has a projectile.
        /// Ranged weapons have projectiles
        /// </summary>
        /// <returns>True if weapon has a projectile</returns>
        public bool HasProjectile() {
            return _projectile != null;
        }

        /// <summary>
        /// Launch projectile to a choosen target
        /// trigged by NPCs
        /// </summary>
        /// <param name="rightHand">right hand transform</param>
        /// <param name="leftHand">left hand transform</param>
        /// <param name="target">target to shoot projectile at</param>
        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target) {
            Projectile projectileInstance =
                Instantiate(_projectile,
                            GetTransform(rightHand, leftHand).position,
                            Quaternion.identity);
            projectileInstance.SetTarget(target, _damage);
        }

        /// <summary>
        /// Launch projectile to a choosen target
        /// trigged by Player
        /// </summary>
        /// <param name="rightHand">right hand transform</param>
        /// <param name="leftHand">left hand transform</param>
        /// <param name="forward">target to shoot projectile a</param>
        public void LaunchProjectile(Transform rightHand, Transform leftHand, Vector3 forward) {
            Projectile projectileInstance =
                Instantiate(_projectile,
                            GetTransform(rightHand, leftHand).position,
                            Quaternion.identity);
            projectileInstance.SetTarget(_damage, forward);
        }

        /// <summary>
        /// Get weapon range
        /// </summary>
        /// <returns>weapon range</returns>
        public float GetRange() {
            return _range;
        }

        /// <summary>
        /// Get weapon damage
        /// </summary>
        /// <returns>weapon damage</returns>
        public float GetDamage() {
            return _damage;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Destroys the old weapon
        /// </summary>
        /// <param name="rightHand">player's right hand transform</param>
        /// <param name="leftHand">player's left hand transform</param>
        private void DestroyOldWeapon(Transform rightHand, Transform leftHand) {
            Transform weaponToDestroy = rightHand.Find(_weaponName);
            if(weaponToDestroy == null) {
                weaponToDestroy = leftHand.Find(_weaponName);
            }
            if (weaponToDestroy == null) return;

            weaponToDestroy.name = "DESTROYING";
            Destroy(weaponToDestroy.gameObject);
        }

        /// <summary>
        /// Get transform of correct hand
        /// </summary>
        /// <param name="rightHand">right hand transform</param>
        /// <param name="leftHand">left hand transform</param>
        /// <returns>Left hand transform if weapon is left handed, 
        /// right hand transform if weapon is right handed</returns>
        private Transform GetTransform(Transform rightHand, Transform leftHand) {
            Transform handTransform;
            if (_isRightHanded)
                handTransform = rightHand;
            else
                handTransform = leftHand;
            return handTransform;
        }

        #endregion

    }
}
