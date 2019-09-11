using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats {
    public class StatPoints : MonoBehaviour {

        #region Singleton

        private static StatPoints _instance;
        public static StatPoints Instance { get => _instance; set => _instance = value; }

        private void SetupSingleton() {
            if (_instance != null && _instance != this) {
                Destroy(gameObject);
            }
            else {
                _instance = this;
            }
        }

        #endregion

        private int _points;
        public int Points {
            get => _points;
            set {
                _points = value;
                if (_points <= 0)
                    _points = 0;
            }
        }

        private void Awake() {
            SetupSingleton();
            Points = 0;
        }
    }
}
