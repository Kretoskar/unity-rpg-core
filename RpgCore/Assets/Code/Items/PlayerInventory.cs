using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Items {
    public class PlayerInventory : Inventory {
        [SerializeField]
        private List<Image> _equipSlotsReferenceImgs = new List<Image>();

        private const int _weaponEquipSlotIndex = 23;
        public int WeaponEquipSlotIndex { get; private set; }

        private int _equipSlotAmount = 4;
        public int EquipSlotAmount { get => _equipSlotAmount; set => _equipSlotAmount = value; }

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
            WeaponEquipSlotIndex = _weaponEquipSlotIndex;
        }

        protected override void ExtrasInStart() {
            SetupEquipSlots();
        }

        private void SetupEquipSlots() {
            for(int i = 0; i < _equipSlotAmount; i++) {
                Instantiate(_equipSlotsReferenceImgs[i], Slots[_slotAmount - _equipSlotAmount + i].transform);
                Slots[_slotAmount - _equipSlotAmount + i].AddComponent<EquipSlot>();
            }
        }
    }
}
