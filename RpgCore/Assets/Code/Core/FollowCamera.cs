using UnityEngine;

namespace RPG.Core {
    /// <summary>
    /// Sets up a follow camera for the player
    /// </summary>
    public class FollowCamera : MonoBehaviour {
        [SerializeField] private Transform _target = null;

        private void LateUpdate() {
            transform.position = _target.position;
        }
    }
}
