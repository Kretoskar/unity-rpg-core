using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core {
    /// <summary>
    /// Spawns objects that aren't supposed to be destroyed on load
    /// </summary>
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

        /// <summary>
        /// Spawn persistent objects
        /// </summary>
        private void SpawnPersistentObject() {
            GameObject persistentObject = Instantiate(_persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
