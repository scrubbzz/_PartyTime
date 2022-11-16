using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames.Generic
{
    /// <summary>
    /// Class for minigame scripts to inherit from for minigame-starting, and minigame-ending features, 
    /// eg: locking controls or stopping spawns.
    /// </summary>
    public interface ITimerControllable
    {
        public abstract void OnStartTime(); // start minigame

        public abstract void OnStopTime(); // end minigame

    }
}
