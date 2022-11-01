using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinBox : MonoBehaviour
{
    [SerializeField] GameObject boxParticles;

    private void OnCollisionEnter(Collision collision)
    {
        boxParticles.SetActive(true);
        gameObject.transform.DetachChildren();

        if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.name != "Player")
        {
            return;
        }

        GameManager.inst.IncrementScoreBox();
        
        StartCoroutine(DestroyBoxTimer());
    }

    IEnumerator DestroyBoxTimer()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }

}
