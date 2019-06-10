using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving {
    /// <summary>
    /// Saveable objects
    /// </summary>
    public interface ISaveable {
        object CaptureState();
        void RestoreState(object state);
    }
}
