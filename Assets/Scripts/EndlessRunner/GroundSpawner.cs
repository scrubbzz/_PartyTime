using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    Vector3 spawnPoint;

    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            if (i < 3) { SpawnTile(false); }
            else { SpawnTile(true); }
        }
    }

    public void SpawnTile(bool spawnItems)
    {
        GameObject temp = Instantiate(groundTile, spawnPoint, Quaternion.identity);
        spawnPoint = temp.transform.GetChild(1).transform.position;

        if (spawnItems)
        {
            temp.GetComponent<GroundTile>().SpawnObstacle();
            temp.GetComponent<GroundTile>().SpawnCoins();
            temp.GetComponent<GroundTile>().SpawnCoinBox();
        }
    }

}
