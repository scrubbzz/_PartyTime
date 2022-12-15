using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames.Generic
{
    /// <summary>
    /// Mostly empty for player scripts in minigames to inherit from. Used to transfer player data to and from minigames.
    /// </summary>
    public class PlayerGeneric : MonoBehaviour
    {
        // class for input data 
        // model reference
        MeshFilter meshFilter;
        MeshRenderer meshRenderer;
        public int coinCount;

        public void Initialise(PlayerScenePersistentData data)
        {
            // set the variables in this class to the variables given by the passed data
        }
    }
}
