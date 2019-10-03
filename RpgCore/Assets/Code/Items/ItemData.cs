using RPG.Combat;
using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Items {
    public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler {

        private const int _equipItemsStartIndex = 20;

        private Item _itemInThisSlot;
        private int _amount;
        private int _slotIndex;

        private Vector2 _dragOffset;
        private Tooltip _tooltip;

        public event Action WeaponEquiped; 

        public Inventory Inventory { get; set; }
        public Item ItemInThisSlot { get => _itemInThisSlot; set => _itemInThisSlot = value; }
        public int Amount { get => _amount; set => _amount = value; }
        public int SlotIndex { get => _slotIndex; set => _slotIndex = value; }
        public static List<ItemData> ItemDatas = new List<ItemData>();

        private void Awake() {
            _amount = 0;
            ItemDatas.Add(this);
        }

        private void Start() {
            _tooltip = Tooltip.Instance;
        }

        public void OnPointerDown(PointerEventData eventData) {
            if (_itemInThisSlot != null) {
                _dragOffset = eventData.position - new Vector2(transform.position.x, transform.position.y);
                transform.position = eventData.position - _dragOffset;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
                _tooltip.SetItem(ItemInThisSlot);
                if (SlotIndex >= _equipItemsStartIndex) {
                    Inventory.Slots[SlotIndex].GetComponent<EquipSlot>().IsEquiped = true;
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if(_itemInThisSlot != null) {
                transform.SetParent(transform.parent.parent);
            }
        }

        public void OnDrag(PointerEventData eventData) {
            if (_itemInThisSlot != null) {
                transform.position = eventData.position - _dragOffset;
            }
        }

        public void OnEndDrag(PointerEventData eventData) {
            transform.SetParent(Inventory.Slots[SlotIndex].transform);
            transform.position = Inventory.Slots[SlotIndex].transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (SlotIndex >= _equipItemsStartIndex) {
                Inventory.Slots[SlotIndex].GetComponent<EquipSlot>().IsEquiped = true;
                if (SlotIndex == PlayerInventory.Instance.WeaponEquipSlotIndex) {
                    PlayerController.Instance.GetComponent<Fighter>().EquipWeapon((Weapon)ItemInThisSlot);
                    WeaponEquiped?.Invoke();
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData) {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
