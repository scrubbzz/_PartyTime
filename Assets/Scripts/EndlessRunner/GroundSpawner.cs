using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    Vector3 spawnPoint;

    public int playerCount { get; private set; }

    void Start()
    {
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
        playerCount = players.Length;

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
            GroundTile tile = temp.GetComponent<GroundTile>();
            tile.SpawnObstacle();
            tile.SpawnCoins();
            tile.SpawnCoinBox();
        }
    }

}
