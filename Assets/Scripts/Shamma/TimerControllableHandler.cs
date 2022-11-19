using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames.Generic
{
    /// <summary>
    /// To be used for minigame timers to handle objects when stopping and starting games.
    /// </summary>
    public class TimerControllableHandler : ScriptableObject
    {
        // list to keep track of all timer controllable objects
        List<ITimerControllable> controllables = new List<ITimerControllable>();

        private void OnEnable()
        {
            // get every controllable gameobject
            // (maybe make an event call to add self to the list instead?)
            //GameObject.FindObjectsOfType<ITimerControllable>();
        }

        public void StartObjects()
        {
            foreach (ITimerControllable controllable in controllables)
            {
                controllable.OnStartTime();
            }
        }

        public void StopObjects()
        {
            foreach (ITimerControllable controllable in controllables)
            {
                controllable.OnStopTime();
            }
        }
        
    }
}
