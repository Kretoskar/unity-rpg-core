using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Items {
    public class Inventory : MonoBehaviour {
        [SerializeField]
        private GameObject _inventoryPanel;

        [SerializeField]
        private GameObject _slotPanel = null;

        [SerializeField]
        private GameObject _inventorySlot = null;

        [SerializeField]
        private GameObject _inventoryItem = null;

        [SerializeField]
        private int _slotAmount = 24;

        private int _currentlyCheckedItemIndex;
        private ItemDatabase _itemDatabase;

        public List<Item> Items = new List<Item>();
        public List<GameObject> Slots = new List<GameObject>();

        #region Singleton

        private static Inventory _instance;
        public static Inventory Instance { get => _instance; set => _instance = value; }

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

        private void Start() {
            _itemDatabase = ItemDatabase.Instance;
            for(int i = 0; i < _slotAmount; i++) {
                Items.Add(_itemDatabase.EmptyItem());
                Slots.Add(Instantiate(_inventorySlot));
                Slots[i].GetComponent<InventorySlot>().ID = i;
                Slots[i].transform.SetParent(_slotPanel.transform);
            }

            AddItem("c2ba4b92-212d-4335-9f65-89ce3ce6b26f");
            AddItem("d66d1e01-8433-4bcb-9d1a-a130031586a5");
            AddItem("d66d1e01-8433-4bcb-9d1a-a130031586a5");
            AddItem("d66d1e01-8433-4bcb-9d1a-a130031586a5");
        }

        public void AddItem(string id) {
            Item itemToAdd = _itemDatabase.FetchItemByID(id);
            if(itemToAdd.Stackable && IsInInventory(itemToAdd)) {
                ItemData data = Slots[_currentlyCheckedItemIndex].transform.GetChild(0).GetComponent<ItemData>();
                data.Amount++;
                data.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = data.Amount.ToString();
            } else {
                for (int i = 0; i < Items.Count; i++) {
                    if (Items[i] == null) {
                        Items[i] = itemToAdd;
                        //Instantiate item icon
                        GameObject itemObject = Instantiate(_inventoryItem);
                        //Set itemData (slot) item to item to add
                        itemObject.GetComponent<ItemData>().ItemInThisSlot = itemToAdd;
                        //Set the slot index to current iterator value
                        itemObject.GetComponent<ItemData>().SlotIndex = i;
                        //Set item position to the slot position
                        itemObject.transform.SetParent(Slots[i].transform);
                        //Set sprite to item's sprite
                        itemObject.GetComponent<Image>().sprite = itemToAdd.Icon;
                        //Center it in slot
                        itemObject.transform.position = Vector2.zero;
                        //Set gameobject name to item name
                        itemObject.name = itemToAdd.Name;
                        //For stackable items
                        ItemData data = Slots[i].transform.GetChild(0).GetComponent<ItemData>();
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
            for(int i = 0; i < Items.Count; i++) {
                if (Items[i] == null)
                    continue;
                if(Items[i].ID == item.ID) {
                    _currentlyCheckedItemIndex = i;
                    return true;
                }
            }
            return false;
        }
    }
}
