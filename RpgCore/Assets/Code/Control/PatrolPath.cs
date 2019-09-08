using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control {
    /// <summary>
    /// NPC path to move at
    /// </summary>
    public class PatrolPath : MonoBehaviour {

        const float waypointGizmoRadius = 0.3f;

        #region MonoBehaviour Methods

        private void OnDrawGizmos() {
            int length = transform.childCount;
            for (int i = 0; i < length; i++) {
                int nextIndex = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(nextIndex));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the index of the next child
        /// </summary>
        /// <param name="i">current child index</param>
        /// <returns></returns>
        public int GetNextIndex(int i) {
            if (i + 1 == transform.childCount)
                return 0;
            return i + 1;
        }

        /// <summary>
        /// Get position of waypoint
        /// </summary>
        /// <param name="i">child index</param>
        /// <returns></returns>
        public Vector3 GetWaypoint(int i) {
            return transform.GetChild(i).transform.position;
        }

        #endregion

    }
}
