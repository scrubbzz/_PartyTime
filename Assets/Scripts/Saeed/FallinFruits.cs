using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitKahoot
{
    public class FallinFruits : MonoBehaviour, IfallingCollectable
    {
        private float speed;
        public float minSpeed;
        public float maxSpeed;
        // public Sprite[] sprites;
        //public GameObject[] fallingFruits;
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
            position.x = Random.Range(-11, 12);
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
                Manager.Instance.CollectBombs(gameObject);
            }

            if (collider.gameObject.tag == "Plane")
            {
                Destroy(gameObject);
            }

        }
    }

}
