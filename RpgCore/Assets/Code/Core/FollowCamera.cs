using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    [SerializeField] private Transform _target = null;

    private void LateUpdate() {
        transform.position = _target.position;
    }
}
