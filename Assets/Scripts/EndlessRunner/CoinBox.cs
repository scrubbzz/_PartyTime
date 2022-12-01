using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinBox : MonoBehaviour
{

    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] GameObject boxParticles;

    [SerializeField] int valueInCoins = 10;


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == obstacleLayer)
        {
            Destroy(gameObject);
            return;
        }

        PlayerCoinCounter player = collision.gameObject.GetComponent<PlayerCoinCounter>();

        if (player != null)
        {
            boxParticles.SetActive(true);
            gameObject.transform.DetachChildren();

            player.IncrementScore(valueInCoins);

            Destroy(gameObject, 0.05f);
        }

    }

}
