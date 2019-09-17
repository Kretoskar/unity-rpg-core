using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Item {
    public class ItemData : MonoBehaviour {
        private Item _itemInThisSlot;
        private int _amount;

        public Item ItemInThisSlot { get => _itemInThisSlot; set => _itemInThisSlot = value; }
        public int Amount { get => _amount; set => _amount = value; }

        private void Awake() {
            _amount = 0;
        }
    }
}
