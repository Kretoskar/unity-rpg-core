using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player input
/// </summary>
public class PlayerController : MonoBehaviour {

    private Mover mover;

    private void Update() {
        if(Input.GetMouseButton(0)) {
            MoveToCursor();
        }
    }

    private void Start() {
        mover = GetComponent<Mover>();
    }

    /// <summary>
    /// Move character to raycast hit position
    /// </summary>
    private void MoveToCursor() {
        Ray ray = RecalculateRaycast();
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit) {
            mover.MoveTo(hit.point);
        }
    }

    /// <summary>
    /// Calculatre ray of the mouse click
    /// </summary>
    /// <returns> Ray of the mouse click </returns>
    private Ray RecalculateRaycast() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

}
