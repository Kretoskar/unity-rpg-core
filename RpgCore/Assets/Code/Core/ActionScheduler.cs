using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core {
    /// <summary>
    /// Handles changing between player's actions
    /// </summary>
    public class ActionScheduler : MonoBehaviour {

        private IAction _currentAction;

        /// <summary>
        /// Start the given action
        /// </summary>
        /// <param name="action">Action to start</param>
        public void StartAction(IAction action) {
            if (_currentAction == action) return;
            if (_currentAction != null)
                _currentAction.Cancel();
            _currentAction = action;
        }
    }
}
