using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control {
    /// <summary>
    /// Handles player's character interaction
    /// </summary>
    public class PlayerController : MonoBehaviour {


        private void Update() {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }


        /// <summary>
        /// Interact with combat target
        /// </summary>
        private bool InteractWithCombat() {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits) {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0)) {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check for player input to handle movement
        /// </summary>
        private bool InteractWithMovement() {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit) {
                if (Input.GetMouseButton(0)) {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Calculatre ray of the mouse click
        /// </summary>
        /// <returns> Ray of the mouse click </returns>
        private Ray GetMouseRay() {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}