//using Codice.Client.BaseCommands.CheckIn.Progress;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;

    [SerializeField] int reductionValue = 5; // how much to decrease player score by

    private void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerCoinCounter>().DecreaseScore(reductionValue);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        //slow down player
    //        other.gameObject.GetComponent<PlayerCoinCounter>().DecreaseScore(reductionValue);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    //speed player back up
    //}
}
