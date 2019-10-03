using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Items {
    public class Tooltip : MonoBehaviour {
        [SerializeField]
        private Text _tooltipTextGo;
        [SerializeField]
        private Color _commonItemNameColor;
        [SerializeField]
        private Color _uncommonItemNameColor;
        [SerializeField]
        private Color _rareItemNameColor;
        [SerializeField]
        private Color _legendaryItemNameColor;
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
            switch (_currentItem.Rarity) {
                case RarityLevel.Common:
                    _data = "<color=#" + ColorUtility.ToHtmlStringRGB(_commonItemNameColor) + "><b>" + _currentItem.Name + "</b></color>\n\n" +
                        "<color=#" + ColorUtility.ToHtmlStringRGB(_descriptionColor) + ">" + _currentItem.Description + "</color>";
                    break;

                case RarityLevel.Uncommon:
                    _data = "<color=#" + ColorUtility.ToHtmlStringRGB(_uncommonItemNameColor) + "><b>" + _currentItem.Name + "</b></color>\n\n" +
                        "<color=#" + ColorUtility.ToHtmlStringRGB(_descriptionColor) + ">" + _currentItem.Description + "</color>";
                    break;

                case RarityLevel.Rare:
                    _data = "<color=#" + ColorUtility.ToHtmlStringRGB(_rareItemNameColor) + "><b>" + _currentItem.Name + "</b></color>\n\n" +
                        "<color=#" + ColorUtility.ToHtmlStringRGB(_descriptionColor) + ">" + _currentItem.Description + "</color>";
                    break;

                case RarityLevel.Legendary:
                    _data = "<color=#" + ColorUtility.ToHtmlStringRGB(_legendaryItemNameColor) + "><b>" + _currentItem.Name + "</b></color>\n\n" +
                        "<color=#" + ColorUtility.ToHtmlStringRGB(_descriptionColor) + ">" + _currentItem.Description + "</color>";
                    break;
            }
            _tooltipTextGo.text = _data;
        }
    }
}
