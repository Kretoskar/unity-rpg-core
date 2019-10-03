using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Items {
    public class EquipSlot : MonoBehaviour {

        private Image _weaponReferenceImage;

        private bool _isEquiped;
        public bool IsEquiped {
            get {
                return _isEquiped;
            }
            set {
                _isEquiped = value;
                _weaponReferenceImage.gameObject.SetActive(!_weaponReferenceImage.gameObject.activeSelf);
            }
        }

        private void Start() {
            _weaponReferenceImage = transform.GetChild(0).GetComponent<Image>();
        }

    }
}
