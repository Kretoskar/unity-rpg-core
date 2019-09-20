using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.UI;

namespace RPG.Items {
    public class Inventory : MonoBehaviour {
        [SerializeField]
        protected GameObject _inventoryPanel;

        [SerializeField]
        protected GameObject _slotPanel = null;

        [SerializeField]
        protected GameObject _inventorySlot = null;

        [SerializeField]
        protected GameObject _inventoryItem = null;

        [SerializeField]
        protected int _slotAmount = 24;

        protected int _currentlyCheckedItemIndex;
        protected ItemDatabase _itemDatabase;

        public List<Item> Items = new List<Item>();
        public List<GameObject> Slots = new List<GameObject>();

        private void Start() {
            _itemDatabase = ItemDatabase.Instance;
            for(int i = 0; i < _slotAmount; i++) {
                Items.Add(_itemDatabase.EmptyItem());
                Slots.Add(Instantiate(_inventorySlot));
                Slots[i].GetComponent<InventorySlot>().ID = i;
                Slots[i].GetComponent<InventorySlot>().Inventory = this;
                Slots[i].transform.SetParent(_slotPanel.transform);
                //print(Slots[i].GetComponent<InventorySlot>().Inventory);
            }
            UIController.Instance.HideOrShowInventoryUI();
        }

        public void AddItem(string id) {
            Item itemToAdd = _itemDatabase.FetchItemByID(id);
            if(itemToAdd.Stackable && IsInInventory(itemToAdd)) {
                ItemData data = Slots[_currentlyCheckedItemIndex].transform.GetChild(0).GetComponent<ItemData>();
                data.Inventory = this;
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
                        itemObject.transform.localPosition = Vector2.zero;
                        //Set gameobject name to item name
                        itemObject.name = itemToAdd.Name;
                        //For stackable items
                        ItemData data = Slots[i].transform.GetChild(0).GetComponent<ItemData>();
                        data.Inventory = this;
                        data.Amount = 1;
                        print(itemObject.transform.position);
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
        public bool IsInInventory(Item item) {
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
