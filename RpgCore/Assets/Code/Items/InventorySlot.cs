using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Items {
    public class InventorySlot : MonoBehaviour, IDropHandler {
        public Inventory Inventory { get; set; }
        public int ID { get; set; }

        public void OnDrop(PointerEventData eventData) {
            ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
            if(Inventory.Items[ID] == null) {
                Inventory.Items[droppedItem.SlotIndex] = null;
                Inventory.Items[ID] = droppedItem.ItemInThisSlot;
                droppedItem.SlotIndex = ID;
            } else if(droppedItem.SlotIndex != ID) {
                //Swap items
                Transform item = transform.GetChild(0);
                print(item.GetComponent<ItemData>());
                item.GetComponent<ItemData>().SlotIndex = droppedItem.SlotIndex;
                item.transform.SetParent(Inventory.Slots[droppedItem.SlotIndex].transform);
                item.transform.position = Inventory.Slots[droppedItem.SlotIndex].transform.position;

                droppedItem.SlotIndex = ID;
                droppedItem.transform.SetParent(transform);
                droppedItem.transform.position = transform.position;

                Inventory.Items[droppedItem.SlotIndex] = item.GetComponent<ItemData>().ItemInThisSlot;
                Inventory.Items[ID] = droppedItem.ItemInThisSlot;
            }
        }
    }
}
