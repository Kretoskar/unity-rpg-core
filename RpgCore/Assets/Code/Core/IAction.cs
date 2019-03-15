using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core {
    /// <summary>
    /// Actions for the Action Scheduler to handle
    /// </summary>
    public interface IAction {
        void Cancel();
    }
}
