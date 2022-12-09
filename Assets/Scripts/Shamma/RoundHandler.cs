using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames.Generic
{
    public class RoundHandler : MonoBehaviour
    {
        static List<PlayerScenePersistentData> players = new List<PlayerScenePersistentData>();

        SceneSwitchingHandler sceneSelector;
        public static RoundHandler roundHandler { get; private set; }

        private void Start()
        {
            roundHandler = this;
        }

        // called when all players press ready and the round begins
        public void StartRound()
        {
            sceneSelector = new SceneSwitchingHandler();
            sceneSelector.BeginLoadMinigame(players.ToArray());
        }

        public void AdvanceRound()
        {
            // add points to players' scene contiguous data from the player controllers

            // go to next screen
            sceneSelector.DetermineAndLoadNextScene(players.ToArray());
        }
    }
}
