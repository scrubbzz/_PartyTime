using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames.Generic
{ 
    /// <summary>
    /// class that allows users to get references to the players in a minigame scene and the winner of a given minigame.
    /// </summary>
    public class PlayerSelector
    {
        static PlayerGeneric[] playersInScene; // initialised when GetPlayers is called.

        // call in the minigames after time ends. returns player with the highest scorep
        public static GameObject GetRoundWinner()
        {
            if (playersInScene == null) GetPlayersInScene();

            GameObject player = null;
            int coins = 0;

            for (int i = 0; i < playersInScene.Length; i++)
            {
                if (playersInScene[i].coinCount > coins)
                {
                    coins = playersInScene[i].coinCount;
                    player = playersInScene[i].gameObject;
                }
            }

            return player;
        }

        public static PlayerGeneric[] GetPlayersInScene()
        {
            GameObject[] controllerObjs = GameObject.FindGameObjectsWithTag("Player");
            playersInScene = new PlayerGeneric[controllerObjs.Length];

            for (int i = 0; i < controllerObjs.Length; i++)
            {
                playersInScene[i] = controllerObjs[i].GetComponent<PlayerGeneric>();
            }

            return playersInScene;
        }
    }
}