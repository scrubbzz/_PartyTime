using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitKahoot
{
    public class FallinFruits : MonoBehaviour
    {
        private float speed;
        public float minSpeed;
        public float maxSpeed;
        // public Sprite[] sprites;
        //public GameObject[] fallingFruits;
        void OnEnable()
        {

            Vector3 position = transform.position;
            position.x = Random.Range(-11, 12);
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
                Manager.Instance.CollectFruits(gameObject);
            }

            if (collision.gameObject.tag == "Plane")
            {
                Destroy(gameObject);
            }

        }
    }

}
