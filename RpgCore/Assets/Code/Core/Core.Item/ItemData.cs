using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Item {
    public class ItemData : MonoBehaviour {
        private Item _item;
        private int _amount;
        public int Amount { get { return _amount; } set { _amount = value; } }

        private void Awake() {
            _amount = 0;
        }
    }
}
