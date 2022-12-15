using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames.Generic
{
    /// <summary>
    /// For minigame timers to inherit from, in order to handle objects when stopping and starting games. this class
    /// inherits from MonoBehaviour for access to Start and Update.
    /// </summary>
    public abstract class TimeControllableHandler : MonoBehaviour
    {
        protected bool tickingActive;
        public static float timePassed { get; protected set; }
        public static float timeRemaining { get; protected set; }

        protected float timeLimit = 60f; // value for inheriting timers to determine when to call StopObjects


        // list to keep track of all timer controllable objects
        //protected List<ITimerControllable> controllables;

        // events for timer controllables to subscribe their interface functions to
        public delegate void OnCountingDownHandler();
        public static OnCountingDownHandler OnCountingDown;

        public delegate void OnStartingHandler();
        public static OnStartingHandler OnStarting;

        public delegate void OnStoppingHandler();
        public static OnStoppingHandler OnStopping;


        //private void OnEnable()
        //{
            // get every controllable gameobject
            // (maybe make an event call to add self to the list instead?)
            //GameObject.FindObjectsOfType<ITimerControllable>();
        //}

        // logic for stuff between seconds here. increment time, update UI etc
        protected virtual void Countdown()
        {
            timePassed += Time.deltaTime;
            timeRemaining -= Time.deltaTime;

            if (OnCountingDown != null) { OnCountingDown(); }
        }

        protected virtual void StartObjects()
        {
            if (OnStarting != null) { OnStarting(); }

            /*foreach (ITimerControllable controllable in controllables)
            {
                controllable.OnStartTime();
            }*/
        }

        protected virtual void StopObjects()
        {
            if (OnStopping != null) { OnStopping(); }
        }
        
    }
}
