using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Items {
    public class PlayerInventory : Inventory {

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
    }
}
