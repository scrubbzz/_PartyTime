using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitKahoot
{
    public class FallinBombs : MonoBehaviour
    {
        private float speed;
        public float minSpeed;
        public float maxSpeed;
        //public GameObject[] bombs;
        void OnEnable()
        {
            Vector3 position = transform.position;
            position.x = Random.Range(-9, 10);
            speed = Random.Range(minSpeed, maxSpeed);
            transform.position = position;
            
        }

        void Update()
        {
            Vector3 position = transform.position;
            position.y = position.y - speed * Time.deltaTime;
            transform.position = position;

            

        }
        public void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Manager.Instance.CollectBombs(gameObject);
            }

            if (collision.gameObject.tag == "Plane")
            {
                Destroy(gameObject);
            }
        }
    }
}


