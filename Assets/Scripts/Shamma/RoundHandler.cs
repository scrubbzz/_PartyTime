using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames.Generic
{
    public class RoundHandler : MonoBehaviour
    {
        static List<PlayerScenePersistentData> players = new List<PlayerScenePersistentData>();

        SceneSwitchingHandler sceneSelector = new SceneSwitchingHandler();
        public static RoundHandler roundHandler { get; private set; }

        private void Start()
        {
            roundHandler = this;

            if (FindObjectOfType<TimeControllableHandler>())
            {
                TimeControllableHandler.OnStopping += UpdatePlayerData;
            }
        }

        // called when all players press ready and the round begins
        public void StartRound()
        {
            //sceneSelector = new SceneSwitchingHandler();
            sceneSelector.DetermineAndLoadNextScene(players.ToArray());
        }

        public void AdvanceRound()
        {
            // add points to players' scene contiguous data from the player controllers


            // go to next screen
            sceneSelector.DetermineAndLoadNextScene(players.ToArray());
        }

        void UpdatePlayerData()
        {
            PlayerGeneric[] generics = PlayerSelector.GetPlayersInScene();

            for (int i = 0; i < players.Count; i++)
            {
                players[i].coinsEarned = generics[i].coinCount;
            }

            StartCoroutine(DisplayWinnerAndContinue());
        }

        IEnumerator DisplayWinnerAndContinue()
        {
            PlayerSelector.GetRoundWinner();

            yield return new WaitForSeconds(1);

            AdvanceRound();
        }

    }
}
