using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minigames.Generic
{
    /// <summary>
    /// Class to get player controllers in a scene as it loads, and pass individual player data to them.
    /// </summary>
    public class MinigameLoader
    {

        // function called by sceneswitching handler to get players into game
        public static void LoadandSetupMinigame(PlayerScenePersistentData[] players, string sceneName)
        {
            // load scene
            RoundHandler.DontDestroyOnLoad(RoundHandler.roundHandler);
            SceneManager.LoadScene(sceneName);

            // if there are more scene objs than playerdata objects, destroy the extra scene objs
            ClearExtraControllerObjects(players);

            // get list of all genericplayer objs in scene
            PlayerGeneric[] playerControllers = PlayerSelector.GetPlayersInScene(); // GetPlayerControllers();

            // hook up each playerdata's input values to each genericplayer obj (call the function from genericplayer)
            // ensure models are loaded
            for (int i = 0; i < playerControllers.Length; i++) 
            { 
                playerControllers[i].Initialise(players[i]); 
            }

        }

        static void ClearExtraControllerObjects(PlayerScenePersistentData[] players)
        {
            GameObject[] controllerObjs = GameObject.FindGameObjectsWithTag("Player");

            if (controllerObjs.Length > players.Length)
            {
                // destroy objects in the array past the length of the player array (objects are not needed if there's no player to use them)
                for (int i = players.Length; i < controllerObjs.Length; i++) 
                { 
                    Object.Destroy(controllerObjs[i]); 
                }
            }
        }

        /*static PlayerGeneric[] GetPlayerControllers()
        {
            GameObject[] controllerObjs = GameObject.FindGameObjectsWithTag("Player");
            PlayerGeneric[] controllers = new PlayerGeneric[controllerObjs.Length];

            for (int i = 0; i < controllerObjs.Length; i++)
            {
                controllers[i] = controllerObjs[i].GetComponent<PlayerGeneric>();
            }
            
            return controllers;
        }*/

        static void PassPlayersToNextScene()
        {
            // update coin counts, clear genericplayer list

            // dontdestroyonload this gameobject

            // call coresceneloader's loadnextscene func (function determines what scene needs loaded before loading)
        }
    }
}
