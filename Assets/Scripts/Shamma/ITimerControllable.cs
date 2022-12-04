using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames.Generic
{
    /// <summary>
    /// Class for minigame scripts to inherit from for minigame-starting, and minigame-ending features, 
    /// eg: locking controls or stopping spawns. Functions should be subscribed to the corresponding events.
    /// </summary>
    public interface ITimerControllable
    {
        public void OnStartTime(); // start minigame

        public void OnStopTime(); // end minigame

        public virtual void OnTimerTick() { } // every time a timer second passes

    }
}
