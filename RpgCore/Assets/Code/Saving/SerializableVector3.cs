using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving {
    /// <summary>
    /// Vector3 object that can be serializable
    /// </summary>
    [System.Serializable]
    public class SerializableVector3 {
        float x, y, z;

        public SerializableVector3(Vector3 vector) {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector() {
            return new Vector3(x,y,z);
        }
    }
}
