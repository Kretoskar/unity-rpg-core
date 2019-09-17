using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Item {
    public class ItemDatabase : MonoBehaviour {
        [SerializeField]
        private List<Item> _database = new List<Item>();

        public List<Item> Database { get { return _database; } }

        #region Singleton

        private static ItemDatabase _instance;
        public static ItemDatabase Instance { get => _instance; set => _instance = value; }

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

        public Item FetchItemByID(string id) {
            foreach (Item item in Database) {
                if (item.ID == id) {
                    return item;
                }
            }
            Debug.LogWarning("Can't find an item with id: " + id);
            return null;
        }

        public Item EmptyItem() {
            return null;
        }
    }
}