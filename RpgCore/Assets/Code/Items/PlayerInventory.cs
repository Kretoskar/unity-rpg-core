using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Items {
    public class PlayerInventory : Inventory {

        [SerializeField]
        private GameObject _weaponSlot = null;

        [SerializeField]
        private GameObject _secondaryWeaponSlot = null;

        [SerializeField]
        private GameObject _helmetSlot = null;

        [SerializeField]
        private GameObject _armorSlot = null;

        [SerializeField]
        private GameObject _gloveSlot = null;

        [SerializeField]
        private GameObject _bootsSlot = null;

        #region Singleton

        private static PlayerInventory _instance;
        public static PlayerInventory Instance { get => _instance; set => _instance = value; }

        private void SetupSingleton() {
            if (_instance != null && _instance != this) {
                Destroy(gameObject);
            }
            else {
                _instance = this;
            }
        }

        #endregion

        private void Awake() {
            SetupSingleton();
        }

        protected override void ExtrasInStart() {
            _itemDatabase = ItemDatabase.Instance;
            EquipSlots.Add(_weaponSlot);
            EquipSlots.Add(_secondaryWeaponSlot);
            EquipSlots.Add(_helmetSlot);
            EquipSlots.Add(_armorSlot);
            EquipSlots.Add(_gloveSlot);
            EquipSlots.Add(_bootsSlot);
            for(int i = 0; i < 100; i++) {
                EquipItems.Add(ItemDatabase.Instance.EmptyItem());
            }
            for(int i = 0; i < EquipSlots.Count; i ++) {
                EquipItems.Add(_itemDatabase.EmptyItem());
                EquipSlots[i].GetComponent<EquipSlot>().ID = i + 100;
                EquipSlots[i].GetComponent<EquipSlot>().Inventory = this;
            }
        }
    }
}
