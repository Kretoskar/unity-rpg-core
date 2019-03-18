using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement {
    /// <summary>
    /// Move character and update his animator
    /// </summary>
    public class Mover : MonoBehaviour, IAction {

        private NavMeshAgent _navMeshAgent;
        private ActionScheduler _actionScheduler;
        private Health _health;

        private const string _animatorBlendValue = "ForwardSpeed";

        private void Start() {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _health = GetComponent<Health>();
        }

        private void Update() {
            _navMeshAgent.enabled = !_health.IsDead;
            UpdateAnimator();
        }

        /// <summary>
        /// Start the action of moving the character
        /// </summary>
        /// <param name="destination">destination to move to</param>
        public void StartMoveAction(Vector3 destination) {
            _actionScheduler.StartAction(this);
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