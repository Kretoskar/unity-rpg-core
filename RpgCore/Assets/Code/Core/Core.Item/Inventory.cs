using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Core.Item {
    public class Inventory : MonoBehaviour {
        [SerializeField]
        private GameObject _inventoryPanel;

        [SerializeField]
        private GameObject _slotPanel;

        [SerializeField]
        private GameObject _inventorySlot;

        [SerializeField]
        private GameObject _inventoryItem;

        [SerializeField]
        private int _slotAmount = 24;

        private ItemDatabase _itemDatabase;
        public List<Item> _items = new List<Item>();
        public List<GameObject> _slots = new List<GameObject>();

        private void Start() {
            _itemDatabase = ItemDatabase.Instance;
            for(int i = 0; i < _slotAmount; i++) {
                _items.Add(_itemDatabase.EmptyItem());
                _slots.Add(Instantiate(_inventorySlot));
                _slots[i].transform.SetParent(_slotPanel.transform);
            }

            AddItem("c2ba4b92-212d-4335-9f65-89ce3ce6b26f");
            AddItem("d66d1e01-8433-4bcb-9d1a-a130031586a5");
            AddItem("d66d1e01-8433-4bcb-9d1a-a130031586a5");
            AddItem("d66d1e01-8433-4bcb-9d1a-a130031586a5");
        }

        public void AddItem(string id) {
            Item itemToAdd = _itemDatabase.FetchItemByID(id);
            if(itemToAdd.Stackable && IsInInventory(itemToAdd)) {
                for(int i = 0; i < _items.Count; i++) {
                    if(_items[i].ID == id) {
                        ItemData data = _slots[i].transform.GetChild(0).GetComponent<ItemData>();
                        data.Amount++;
                        data.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = data.Amount.ToString();
                        break;
                    }
                }
            } else {
                for (int i = 0; i < _items.Count; i++) {
                    if (_items[i] == null) {
                        _items[i] = itemToAdd;
                        //Instantiate item icon
                        GameObject itemObject = Instantiate(_inventoryItem);
                        //Set item position to the slot position
                        itemObject.transform.SetParent(_slots[i].transform);
                        //Set sprite to item's sprite
                        itemObject.GetComponent<Image>().sprite = itemToAdd.Icon;
                        //Center it in slot
                        itemObject.transform.position = Vector2.zero;
                        //Set gameobject name to item name
                        itemObject.name = itemToAdd.Name;
                        //For stackable items
                        ItemData data = _slots[i].transform.GetChild(0).GetComponent<ItemData>();
                        data.Amount = 1;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if item is already in inventory
        /// </summary>
        /// <param name="item">item to check</param>
        /// <returns>true if item is in inventry</returns>
        private bool IsInInventory(Item item) {
            for(int i = 0; i < _items.Count; i++) {
                if (_items[i] == null)
                    continue;
                if(_items[i].ID == item.ID) {
                    return true;
                }
            }
            return false;
        }
    }
}
