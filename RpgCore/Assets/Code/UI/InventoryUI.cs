using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Stats;
using RPG.Combat;
using RPG.Control;
using RPG.Items;

namespace RPG.UI {
    public class InventoryUI : MonoBehaviour {
        [SerializeField]
        private Text _damageText = null;

        [SerializeField]
        private Text _powerText = null;

        [SerializeField]
        private Text _armorText = null;

        private PlayerStats _playerStats;
        private Fighter _playerFighter;

        private void Start() {
            _playerStats = PlayerStats.Instance;
            _playerFighter = PlayerController.Instance.GetComponent<Fighter>(); 
            _playerStats.DurabilityChanged += UpdateUI;
            _playerStats.StrengthChanged += UpdateUI;
            _playerStats.PowerChanged += UpdateUI;

            foreach(ItemData itemData in ItemData.ItemDatas) {
                print(itemData);
                itemData.WeaponEquiped += UpdateUI;
            }
        }

        private void UpdateUI() {
            _damageText.text = GlobalStats.AttackDamage(_playerFighter.CurrentWeapon.GetDamage(), _playerStats.Strength).ToString();
            _powerText.text = GlobalStats.MagicDamage(_playerStats.Power).ToString();
            _armorText.text = "0";
        }

    }
}
