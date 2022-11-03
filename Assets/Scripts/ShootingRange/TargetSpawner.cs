using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetSpawner : MonoBehaviour
{

    public bool spawningIsActive;

    public float spawnDistanceOnZ = 5;
    public float minPossibleSpawnTime = 5;
    public float maxPossibleSpawnTime = 5;
    float approximateSpawnTime = 5;

    public float gameLength = 60;
    float spawnTimer;

    public List<GameObject> spawnableTargets = new List<GameObject>();


    public TextMeshProUGUI testTimer;


    private void Update()
    {
        testTimer.text = "" + gameLength;

        spawnTimer += Time.deltaTime;


        if (spawningIsActive)
        {
            gameLength -= Time.deltaTime;

            if (spawnTimer > approximateSpawnTime)
            {
                GenerateTarget();
                spawnTimer = 0;
                approximateSpawnTime = Random.Range(minPossibleSpawnTime, maxPossibleSpawnTime);
            }
        }

        if (gameLength <= 0 && spawningIsActive)
        {
            spawningIsActive = false;
            Debug.Log("Time's up!");
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
        Vector3 offscreenPosition = Camera.main.ViewportToWorldPoint(new Vector3(targetPosOnX, targetPosOnY, spawnDistanceOnZ));

        GameObject generatedTarget = Instantiate(spawnableTargets[randomTargetType], 
                                                 offscreenPosition, 
                                                 Quaternion.identity);

        if (generatedTarget == null)
            Debug.LogError("something went wrong with spawning the target.");
        else
            generatedTarget.AddComponent<ShooterTarget>();

        
    }
}
