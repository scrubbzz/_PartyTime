using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ***

// DEPRECATED
// inherit from TimerControllableHandler instead.

// ***
namespace Minigames.Generic
{ 
    /// <summary>
    /// Used to keep track of and display time remaining in the minigame, and call object behaviours accordingly.
    /// </summary>
    public class MinigameTimer : MonoBehaviour
    {

        TimerControllableHandler objectHandler;

        float remainingTime;
        float totalTime;

        void Start()
        {
            //objectHandler = new TimerControllableHandler();

            // start all objects after game begins
        }

        void Update()
        {
            Countdown();
        }

        void Countdown()
        {
            // decrease current time left

            // if time reaches certain point, set game objects to stop
            // wait a moment before sending a command to the minigame loader to load next scene
        }


    }
}
