using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control {
    /// <summary>
    /// Handles player's character interaction
    /// </summary>
    public class PlayerController : MonoBehaviour {

        private Mover mover;

        private void Update() {
            InterractWithMovement();
        }

        private void Start() {
            mover = GetComponent<Mover>();
        }

        /// <summary>
        /// Check for player input to handle movement
        /// </summary>
        private void InterractWithMovement() {
            if (Input.GetMouseButton(0)) {
                InteractWithCombat();
                MoveToCursor();
            }
        }

        /// <summary>
        /// Interact with combat target
        /// </summary>
        private void InteractWithCombat() {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits) {
                CombatTarget target =  hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if(Input.GetMouseButtonDown(0)) {
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }

        /// <summary>
        /// Move character to raycast hit position
        /// </summary>
        private void MoveToCursor() {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit) {
                mover.MoveTo(hit.point);
            }
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