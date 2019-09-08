using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement {
    /// <summary>
    /// Move character and update his animator
    /// </summary>
    [RequireComponent(typeof(PlayerMover))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ActionScheduler))]
    [RequireComponent(typeof(Health))]
    public class Mover : MonoBehaviour, IAction, ISaveable {

        private const string _animatorBlendValue = "ForwardSpeed";

        private NavMeshAgent _navMeshAgent;
        private ActionScheduler _actionScheduler;
        private Health _health;
        private PlayerMover _playerMover;

        private bool _isPlayer = false;

        #region MonoBehaviour Methods

        private void Awake() {
            _playerMover = GetComponent<PlayerMover>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _health = GetComponent<Health>();
        }

        private void Update() {
            _navMeshAgent.enabled = !_health.IsDead;
            UpdateAnimator();
        }

        #endregion

        #region Public Methods

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
        /// Move player according to joystick input
        /// </summary>
        /// <param name="horizontal">horizontal joystick input</param>
        /// <param name="vertical">vertical joystick input</param>
        public void MovePlayer(float horizontal, float vertical) {
            _isPlayer = true;
            _playerMover.Move(horizontal, vertical);
        }

        /// <summary>
        /// Stop the player movement
        /// </summary>
        public void StopPlayer() {
            _playerMover.Move(0, 0);
        }

        /// <summary>
        /// Capture state for saving
        /// </summary>
        /// <returns>State for saving</returns>
        public object CaptureState() {
            return new SerializableVector3(transform.position);
        }

        /// <summary>
        /// Restore state to load
        /// </summary>
        /// <param name="state">State to load</param>
        public void RestoreState(object state) {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Update animator that uses blend tree
        /// depending on playerNavMeshAgent velocity z value
        /// </summary>
        private void UpdateAnimator() {
            if (_isPlayer) return;
            Vector3 velocity = _navMeshAgent.velocity;
            // Transfer from global to local
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat(_animatorBlendValue, speed);
        }

        #endregion

    }
}