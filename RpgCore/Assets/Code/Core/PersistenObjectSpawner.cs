using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core {
    public class PersistenObjectSpawner : MonoBehaviour {
        [SerializeField]
        private GameObject _persistentObjectPrefab = null;

        private static bool _hasSpawned = false;

        private void Awake() {
            if (_hasSpawned)
                return;

            SpawnPersistentObject();

            _hasSpawned = true;
        }

        private void SpawnPersistentObject() {
            GameObject persistentObject = Instantiate(_persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
