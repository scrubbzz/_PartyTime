using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StraightShootin
{
    /// <summary>
    /// Spawns objects within its spawnableTargets list at the edges of the screen, after a random interval of time passes.
    /// </summary>
    public class TargetSpawner : MonoBehaviour, Minigames.Generic.ITimeControllable
    {

        bool spawningIsActive = false;

        [SerializeField] float spawnDistanceOnZ = 5f;
        [SerializeField] float spawnDistanceFudging = 2.5f; // amount of possible variance for the spawn distance,
                                                            // more means targets can spawn further forward and backward from the main spawn distance
        [SerializeField] float upperScreenLimit = 0.75f;
        [SerializeField] float lowerScreenLimit = 0.3f;


        [SerializeField] float minPossibleSpawnTime = 1.7f;
        [SerializeField] float maxPossibleSpawnTime = 3f;

        float spawnTimeCounter;
        float timeToNextSpawn = 0f;


        public List<GameObject> spawnableTargets = new List<GameObject>();

        private void Start()
        {
            Time.timeScale = 1f;
        }
        // subscribe and unsubscribe to timer events
        private void OnEnable()
        {
            if (FindObjectOfType<ShootingRangeTimer>())
            {
                ShootingRangeTimer.OnStarting += OnStartTime;
                ShootingRangeTimer.OnCountingDown += OnTimerTick;
                ShootingRangeTimer.OnStopping += OnStopTime;
            }

            Physics.gravity = new Vector3(0, -10f); // I shouldn't be stuffing this in here but there's no great place to put it?
        }
        private void OnDisable()
        {
            if (FindObjectOfType<ShootingRangeTimer>())
            {
                ShootingRangeTimer.OnStarting -= OnStartTime;
                ShootingRangeTimer.OnCountingDown -= OnTimerTick;
                ShootingRangeTimer.OnStopping -= OnStopTime;
            }
        }


        public void OnStartTime() { spawningIsActive = true; }
        public void OnStopTime() { spawningIsActive = false; }


        public void OnTimerTick()
        {
            if (spawningIsActive && ShootingRangeTimer.timePassed > timeToNextSpawn)
            {
                GenerateTarget();
                timeToNextSpawn = Random.Range(minPossibleSpawnTime, maxPossibleSpawnTime) + ShootingRangeTimer.timePassed;
            }
        }


        // spawn a random gameObject in our list on the edge of the screen, add the target script to it.
        void GenerateTarget()
        {

            // get a random number to select a target in the list
            int randomTargetType = Random.Range(0, spawnableTargets.Count);
            Vector3 offscreenPosition = CalculateSpawnPosition();

            GameObject generatedTarget = Instantiate(spawnableTargets[randomTargetType],
                                                     offscreenPosition,
                                                     Quaternion.identity);

            if (generatedTarget == null)
                Debug.LogError("something went wrong with spawning the target.");
            else if (generatedTarget.GetComponent<BreakableTarget>() == null)
                generatedTarget.AddComponent<BreakableTarget>();

        }


        Vector3 CalculateSpawnPosition()
        {
            // get a random 0-1 value for use with viewport
            int targetPosOnX = Mathf.RoundToInt(Random.value); // round to int for solid 0 or 1 (select a side of the screen to spawn)
            float targetPosOnY = Mathf.Clamp(Random.value, lowerScreenLimit, upperScreenLimit); // clamp these so targets don't spawn offscreen

            // convert values to the normalised viewport space (0 = leftmost, 1 = rightmost)
            float spawnDist = Random.Range(spawnDistanceOnZ - spawnDistanceFudging, spawnDistanceOnZ + spawnDistanceFudging);
            Vector3 offscreenPos = Camera.main.ViewportToWorldPoint(new Vector3(targetPosOnX, targetPosOnY, spawnDist));

            return offscreenPos;
        }


    }
}
