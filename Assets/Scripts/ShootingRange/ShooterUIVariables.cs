using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StraightShootin
{
    /// <summary>
    /// Used in PlayerAim to keep track of the variables displayed onscreen, and pass them to the UI handler. (yet to be implemented)
    /// </summary>
    public class ShooterUIVariables : ScriptableObject
    {

        float chargeValue;
        int coinCount;
        RectTransform crosshair;


        public void UpdateValues(PassablePlayerAimData player)
        {
            chargeValue = player.chargeValue;

        }
    }
}
