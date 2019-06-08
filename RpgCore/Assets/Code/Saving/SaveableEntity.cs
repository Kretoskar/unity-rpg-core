using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.Saving {
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour {
        [SerializeField]
        private string uniqueIdentifier = "";

        public string GetUniqueIdentifier() {
            return "";
        }

        public object CaptureState() {
            print("capturing state for " + GetUniqueIdentifier());
            return null;
        }

        public void RestoreState(object state) {
            print("restoring state for " + GetUniqueIdentifier());
        }

        private void Update() { //run in edit time also bcs of ExecuteAlways
            if (Application.IsPlaying(gameObject))
                return;
            if (string.IsNullOrEmpty(gameObject.scene.path))
                return;
            
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("uniqueIdentifier");

            if(string.IsNullOrEmpty(serializedProperty.stringValue)) {
                serializedProperty.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

        }
    }
}
