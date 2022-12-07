using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StraightShootin
{
    /// <summary>
    /// Spawns objects within its spawnableTargets list at the edges of the screen, after a random interval of time passes.
    /// </summary>
    public class TargetSpawner : MonoBehaviour, Minigames.Generic.ITimerControllable
    {

        bool spawningIsActive = false;

        public float spawnDistanceOnZ = 5f;
        public float spawnDistanceFudging = 2.5f; // amount of possible variance for the spawn distance,
                                                  // more means targets can spawn further forward and backward from the main spawn distance

        public float minPossibleSpawnTime = 1.7f;
        public float maxPossibleSpawnTime = 3f;

        float spawnTimeCounter;
        float timeToNextSpawn = 0f;


        public List<GameObject> spawnableTargets = new List<GameObject>();


        // subscribe and unsubscribe to timer events
        private void OnEnable()
        {
            if (FindObjectOfType<ShootingRangeTimer>())
            {
                ShootingRangeTimer.OnStarting += OnStartTime;
                ShootingRangeTimer.OnStopping += OnStopTime;
            }
        }
        private void OnDisable()
        {
            if (FindObjectOfType<ShootingRangeTimer>())
            {
                ShootingRangeTimer.OnStarting -= OnStartTime;
                ShootingRangeTimer.OnStopping -= OnStopTime;
            }
        }


        public void OnStartTime() { spawningIsActive = true; }
        public void OnStopTime() { spawningIsActive = false; }


        // target spawning intervals are randomised separate from game timer. still need game timer to activate spawner
        private void Update()
        {
            if (spawningIsActive)
            {
                spawnTimeCounter += Time.deltaTime;

                if (spawnTimeCounter > timeToNextSpawn)
                {
                    GenerateTarget();
                    spawnTimeCounter = 0;
                    timeToNextSpawn = Random.Range(minPossibleSpawnTime, maxPossibleSpawnTime);
                }
            }
        }


        // spawn a random gameObject in our list on the edge of the screen, add the target script to it.
        void GenerateTarget()
        {

            // get a random number to select a target in the list
            int randomTargetType = Random.Range(0, spawnableTargets.Count);


            // get a random 0-1 value for use with viewport
            int targetPosOnX = Mathf.RoundToInt(Random.value); // round to int for solid 0 or 1 (select a side of the screen to spawn)
            float targetPosOnY = Mathf.Clamp(Random.value, 0.25f, 0.75f); // clamp these so targets don't spawn offscreen

            // convert values to the normalised viewport space (0 = leftmost, 1 = rightmost)
            float spawnDist = Random.Range(spawnDistanceOnZ - spawnDistanceFudging, spawnDistanceOnZ + spawnDistanceFudging);
            Vector3 offscreenPosition = Camera.main.ViewportToWorldPoint(new Vector3(targetPosOnX, targetPosOnY, spawnDist));

            GameObject generatedTarget = Instantiate(spawnableTargets[randomTargetType],
                                                     offscreenPosition,
                                                     Quaternion.identity);

            if (generatedTarget == null)
                Debug.LogError("something went wrong with spawning the target.");
            else
                generatedTarget.AddComponent<BreakableTarget>();

        }

        
    }
}
