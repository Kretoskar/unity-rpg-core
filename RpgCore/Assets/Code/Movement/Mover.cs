using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement {
    /// <summary>
    /// Move character and update his animator
    /// </summary>
    public class Mover : MonoBehaviour, IAction {

        private NavMeshAgent _navMeshAgent;

        private const string _animatorBlendValue = "ForwardSpeed";

        private void Start() {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update() {
            UpdateAnimator();
        }

        /// <summary>
        /// Start the action of moving the character
        /// </summary>
        /// <param name="destination">destination to move to</param>
        public void StartMoveAction(Vector3 destination) {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        /// <summary>
        /// Move character to given destination
        /// </summary>
        /// <param name="destination">Where to move the character</param>
        public void MoveTo(Vector3 destination) {
            _navMeshAgent.destination = destination;
            _navMeshAgent.isStopped = false;
        }

        /// <summary>
        /// Stop the nav mesh agent
        /// </summary>
        public void Cancel() {
            _navMeshAgent.isStopped = true;
        }

        /// <summary>
        /// Update animator that uses blend tree
        /// depending on playerNavMeshAgent velocity z value
        /// </summary>
        private void UpdateAnimator() {
            Vector3 velocity = _navMeshAgent.velocity;
            // Transfer from global to local
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat(_animatorBlendValue, speed);
        }
    }
}