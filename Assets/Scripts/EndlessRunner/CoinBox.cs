using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinBox : MonoBehaviour
{

    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] GameObject boxFX;
    ParticleSystem boxParticles;
    AudioSource boxAudio;

    [SerializeField] int valueInCoins = 10;

    private void Awake()
    {
        boxParticles = boxFX.GetComponent<ParticleSystem>();
        boxAudio = boxFX.GetComponent<AudioSource>();
    }
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
            boxParticles.Play();
            boxAudio.Play();
            gameObject.transform.DetachChildren();

            player.IncrementScore(valueInCoins);

            Destroy(gameObject);
        }

    }

}
