using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] 
    GameObject[] fruityFruits;

    private void Start()
    {
        StartCoroutine(SpawnFruit(1f));
    }

    IEnumerator SpawnFruit(float time)
    {
        yield return new WaitForSeconds(time);

        Vector3 temp = transform.position;
        temp.x = Random.Range(-11f, 12);

        Instantiate(fruityFruits[Random.Range(0, fruityFruits.Length)], temp, Quaternion.identity);

        StartCoroutine(SpawnFruit(Random.Range(0.5f, 2f)));
    }

}
