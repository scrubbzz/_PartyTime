using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitKahoot
{
    public class FallinBombs : MonoBehaviour, IfallingCollectable
    {
        private float speed;
        public float minSpeed;
        public float maxSpeed;
        //public GameObject[] bombs;
        void OnEnable()
        {
           OnSpawn();
        }

        void Update()
        {
           OnFall();

            

        }
        public void OnTriggerEnter(Collider collision)
        {
           OnTouch(collision);
        }

        public void OnSpawn()
        {
            Vector3 position = transform.position;
            position.x = Random.Range(-9, 10);
            speed = Random.Range(minSpeed, maxSpeed);
            transform.position = position;
        }

        public void OnFall()
        {
            Vector3 position = transform.position;
            position.y = position.y - speed * Time.deltaTime;
            transform.position = position;

        }

        public void OnTouch(Collider collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                Manager manager = collider.GetComponent<Manager>();
                manager.CollectBombs(gameObject);
            }

            if (collider.gameObject.tag == "Plane")
            {
                Destroy(gameObject);
            }

        }
    }
}


