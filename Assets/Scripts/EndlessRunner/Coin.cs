using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;

    [SerializeField] AudioSource audio;
    [SerializeField] GameObject coinValue;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        //coinValue.SetActive(true);
        //gameObject.transform.DetachChildren();
        

        //Instantiate(coinValue);

        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.name != "Player")
        {
            return;
        }

        

        GameManager.inst.IncrementScore();

        Destroy(gameObject);

        audio.Play();
        audio.pitch = Random.Range(0.9f, 1f);
    }
}
