using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving {
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour {
        [SerializeField]
        private string uniqueIdentifier = "unset";

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
            print("xd");
        }
    }
}
