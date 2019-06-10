using RPG.Core;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving {
    /// <summary>
    /// Represents an object that can be saved
    /// </summary>
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour {
        [SerializeField]
        private string _uniqueIdentifier = "";

        static Dictionary<string, SaveableEntity> _globalLookup = new Dictionary<string, SaveableEntity>();

        /// <summary>
        /// Gets the unique identifier
        /// </summary>
        /// <returns>unique identifier</returns>
        public string GetUniqueIdentifier() {
            return _uniqueIdentifier;
        }

        /// <summary>
        /// Capture the object's state to be saved
        /// </summary>
        /// <returns>object's state</returns>
        public object CaptureState() {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach(ISaveable saveable in GetComponents<ISaveable>()) {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        /// <summary>
        /// Restore the object to the state that was saved 
        /// </summary>
        /// <param name="state">what state to restore</param>
        public void RestoreState(object state) {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (ISaveable saveable in GetComponents<ISaveable>()) {
                string typeString = saveable.GetType().ToString();
                if(stateDict.ContainsKey(typeString)) {
                    saveable.RestoreState(stateDict[typeString]);
                }
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Run in edit mode only, updates/creates the UUIDs
        /// </summary>
        private void Update() { //run in edit time also bcs of ExecuteAlways
            if (Application.IsPlaying(gameObject))
                return;
            if (string.IsNullOrEmpty(gameObject.scene.path))
                return;
            
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("_uniqueIdentifier");

            if(string.IsNullOrEmpty(serializedProperty.stringValue) || !IsUnique(serializedProperty.stringValue)) {
                serializedProperty.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
            _globalLookup[serializedProperty.stringValue] = this;
        }
#endif

        /// <summary>
        /// Checks wheter the UUID is actually unique
        /// </summary>
        /// <param name="candidate">UUID to check</param>
        /// <returns>is the UUID unique</returns>
        private bool IsUnique(string candidate) {
            if (!_globalLookup.ContainsKey(candidate))
                return true;
            if (_globalLookup[candidate] == this)
                return true;
            if (_globalLookup[candidate] == null) {
                _globalLookup.Remove(candidate);
                return true;
            }
            //if it's not pointing to the right value
            if(_globalLookup[candidate].GetUniqueIdentifier() != candidate) {
                _globalLookup.Remove(candidate);
                return true;
            }
            return false;
        }
    }
}
