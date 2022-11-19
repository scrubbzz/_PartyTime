using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace StraightShootin
{
    /// <summary>
    /// Spawns objects within its spawnableTargets list at the edges of the screen. 
    /// </summary>
    /// TODO: move time tracking functionality to its own script
    public class TargetSpawner : MonoBehaviour
    {

        public bool spawningIsActive;

        public float spawnDistanceOnZ = 5;

        public float minPossibleSpawnTime = 5;
        public float maxPossibleSpawnTime = 5;
        float approximateSpawnTime = 5;
        float spawnCountdown;

        public float gameLength = 60;
        float originalGameLength = 60;

        public List<GameObject> spawnableTargets = new List<GameObject>();
        List<GameObject> inSceneTargets = new List<GameObject>();


        public TextMeshProUGUI testTimer;


        private void Update()
        {
            // to do: update to use timer inheritance instead. 
            if (spawningIsActive)
            {
                float time = Mathf.FloorToInt(gameLength % 60);
                testTimer.text = "" + time;

                spawnCountdown += Time.deltaTime;

                gameLength -= Time.deltaTime;

                if (spawnCountdown > approximateSpawnTime)
                {
                    GenerateTarget();
                    spawnCountdown = 0;
                    approximateSpawnTime = Random.Range(minPossibleSpawnTime, maxPossibleSpawnTime);
                }
            }

            if (gameLength <= 0 && spawningIsActive)
            {
                spawningIsActive = false;
                

                GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
                PlayerAim[] players = new PlayerAim[playerObjs.Length];

                for (int i = 0; i < playerObjs.Length; i++)
                {
                    players[i] = playerObjs[i].GetComponent<PlayerAim>();
                }

                GameObject player = null;
                int coins = 0;
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].coinCounter > coins)
                    {
                        coins = players[i].coinCounter;
                        player = players[i].gameObject;
                    }
                }

                testTimer.text = "Time's up! \nWinner is: " + player.name;
                //Debug.Log("Time's up!");
            }

            // for testing purposes
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                gameLength = originalGameLength;
                spawningIsActive = true;
            }

        }

        void GenerateTarget()
        {

            // get a random number to select a target in the list
            int randomTargetType = Random.Range(0, spawnableTargets.Count);


            // get a random 0-1 value for use with viewport
            int targetPosOnX = Mathf.RoundToInt(Random.value); // round to int for solid 0 or 1 (select a side of the screen to spawn)
            float targetPosOnY = Mathf.Clamp(Random.value, 0.25f, 0.75f); // clamp these so targets don't spawn offscreen

            // convert values to the normalised viewport space (0 = leftmost, 1 = rightmost)
            float spawnDist = Random.Range(spawnDistanceOnZ - 4, spawnDistanceOnZ + 4);
            Vector3 offscreenPosition = Camera.main.ViewportToWorldPoint(new Vector3(targetPosOnX, targetPosOnY, spawnDist));

            GameObject generatedTarget = Instantiate(spawnableTargets[randomTargetType],
                                                     offscreenPosition,
                                                     Quaternion.identity);

            if (generatedTarget == null)
                Debug.LogError("something went wrong with spawning the target.");
            else
            {
                generatedTarget.AddComponent<ShooterTarget>();

            }

        }
    }
}
