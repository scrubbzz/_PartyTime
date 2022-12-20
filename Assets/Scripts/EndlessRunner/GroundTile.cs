using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;

    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject tallObstaclePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject coinBoxPrefab;

    [SerializeField] float tallObstacleChance = 0.4f;

    [SerializeField] int coinsToSpawn = 8;
    [SerializeField] int boxesToSpawn = 1;

    int triggerCount = 0;
    float despawnTime = 1f;

    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            triggerCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile(true);

        if (triggerCount == groundSpawner.playerCount)
            Destroy(gameObject, despawnTime);
    }

    public void SpawnObstacle()
    {
        GameObject obstacleToSpawn = obstaclePrefab;
        float random = Random.Range(0f, 1f);

        if(random < tallObstacleChance)
        {
            obstacleToSpawn = tallObstaclePrefab;
        }

        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, transform);
    }

    public void SpawnCoins()
    {
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = RandomPointInCollider(GetComponent<Collider>());
        }
    }

    public void SpawnCoinBox()
    {
        for (int i = 0; i < boxesToSpawn; i++)
        {
            GameObject temp = Instantiate(coinBoxPrefab, transform);
            temp.transform.position = RandomPointInColliderBox(GetComponent<Collider>());
        }
    }

    Vector3 RandomPointInCollider(Collider collider)
    {
        
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        if (point != collider.ClosestPoint(point))
        {
            point = RandomPointInCollider(collider);
        }

        point.y = Random.Range(1, 4);
        return point;

    }

    Vector3 RandomPointInColliderBox(Collider collider)
    {

        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        if (point != collider.ClosestPoint(point))
        {
            point = RandomPointInColliderBox(collider);
        }

        point.y = 3;
        return point;

    }
}
