using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Items {
    public class EquipSlot : MonoBehaviour, IDropHandler {
        public PlayerInventory Inventory { get; set; }
        public int ID { get; set; }

        public void OnDrop(PointerEventData eventData) {
            ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
            print(ID);
            if (Inventory.EquipItems[ID] == null) {
                Inventory.EquipItems[droppedItem.SlotIndex] = null;
                Inventory.EquipItems[ID] = droppedItem.ItemInThisSlot;
                droppedItem.SlotIndex = ID;
            }
            else if (droppedItem.SlotIndex != ID) {
                //Swap items
                Transform item = transform.GetChild(0);
                item.GetComponent<ItemData>().SlotIndex = droppedItem.SlotIndex;
                item.transform.SetParent(Inventory.Slots[droppedItem.SlotIndex].transform);
                item.transform.position = Inventory.Slots[droppedItem.SlotIndex].transform.position;

                droppedItem.SlotIndex = ID;
                droppedItem.transform.SetParent(transform);
                droppedItem.transform.position = transform.position;

                Inventory.Items[droppedItem.SlotIndex] = item.GetComponent<ItemData>().ItemInThisSlot;
                Inventory.EquipItems[ID] = droppedItem.ItemInThisSlot;
            }
        }
    }
}
