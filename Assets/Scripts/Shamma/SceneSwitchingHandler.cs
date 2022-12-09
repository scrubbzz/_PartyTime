using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minigames.Generic {
    /// <summary>
    /// Handles the behaviour for selecting which scene to load when a 
    /// </summary>
    public class SceneSwitchingHandler
    {
        List<string> minigameSceneNames = new List<string>() { "EndlessRunner", "SnowGlobalConflict", "FruitKahoot", "StrightShootin" };

        //string[] maxigameSceneNames = new string[] { "EndlessRunner", "SnowGlobalConflict", "FruitKahoot", "StrightShootin" };

        static int completedMinigameCount = 0;
        static int totalMinigamesToComplete = 3;

        // called by the round handler when beginning a round
        public void BeginLoadMinigame(PlayerScenePersistentData[] playerData)
        {
            completedMinigameCount = 0;

            ShuffleMinigameList();
            DetermineAndLoadNextScene(playerData);
        }


        public void DetermineAndLoadNextScene(PlayerScenePersistentData[] playerData)
        {
            // check if we should load a minigame
            if (ShouldLoadMinigame())
            {
                completedMinigameCount++;
                MinigameLoader.LoadandSetupMinigame(playerData, minigameSceneNames[completedMinigameCount]);
                return;
            }
            else
            {
                completedMinigameCount = 0;
                // load results screen and ensure player data isn't destroyed. scene should handle calculating winner
                LoadResultsScreen();
            }
        }


        bool ShouldLoadMinigame()
        {
            // if there's a tie, minus the minigame count so there's one more minigame to play
            if (completedMinigameCount < totalMinigamesToComplete) return true;
            else return false;
        }

        void ShuffleMinigameList()
        {

        }

        void LoadResultsScreen()
        {
            Object.DontDestroyOnLoad(RoundHandler.roundHandler);
            SceneManager.LoadScene("results");
        }


    }
}
