using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames.Generic
{
    /// <summary>
    /// Class used to pass in universal player values from scene to scene, and to the player controllers that inherit from PlayerGeneric.
    /// </summary>
    public class PlayerScenePersistentData
    {
        public Input input;
        public int costumeNumber;
        public int coinsEarned;
    }
}
