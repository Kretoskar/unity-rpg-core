using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Items {
    public class InventorySlot : MonoBehaviour, IDropHandler {

        private Inventory _inventory;

        public int ID { get; set; }

        private void Start() {
            _inventory = Inventory.Instance;
        }

        public void OnDrop(PointerEventData eventData) {
            ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
            print(_inventory);
            if(_inventory.Items[ID] == null || _inventory.Items[ID].ID == null) {
                _inventory.Items[droppedItem.SlotIndex] = null;
                _inventory.Items[ID] = droppedItem.ItemInThisSlot;
                droppedItem.SlotIndex = ID;
            } else {
                //Swap items
                Transform item = transform.GetChild(0);
                item.GetComponent<ItemData>().SlotIndex = droppedItem.SlotIndex;
                item.transform.SetParent(_inventory.Slots[droppedItem.SlotIndex].transform);
                item.transform.position = _inventory.Slots[droppedItem.SlotIndex].transform.position;

                droppedItem.SlotIndex = ID;
                droppedItem.transform.SetParent(transform);
                droppedItem.transform.position = transform.position;

                _inventory.Items[droppedItem.SlotIndex] = item.GetComponent<ItemData>().ItemInThisSlot;
                _inventory.Items[ID] = droppedItem.ItemInThisSlot;
            }
        }
    }
}
