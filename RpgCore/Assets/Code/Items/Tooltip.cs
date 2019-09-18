using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Items {
    public class Tooltip : MonoBehaviour {
        [SerializeField]
        private Text _tooltipTextGo;
        [SerializeField]
        private Color _nameColor;
        [SerializeField]
        private Color _descriptionColor;

        private string _data;

        private Item _currentItem;

        #region Singleton

        private static Tooltip _instance;
        public static Tooltip Instance { get => _instance; set => _instance = value; }

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

        public void SetItem(Item item) {
            _currentItem = item;
            ConstructDataString();
        }

        private void ConstructDataString() {
            _data = "<color=#" + ColorUtility.ToHtmlStringRGB(_nameColor) + "><b>" + _currentItem.Name + "</b></color>\n\n" +
                "<color=#" + ColorUtility.ToHtmlStringRGB(_descriptionColor) + ">" + _currentItem.Description + "</color>";
            _tooltipTextGo.text = _data;
        }
    }
}
