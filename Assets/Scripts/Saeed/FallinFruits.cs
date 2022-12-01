using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitKahoot
{
    public class FallinFruits : MonoBehaviour
    {
        public float speed;
        public Sprite[] sprites;
        //public GameObject[] fallingFruits;
        void Start()
        {
            Vector3 position = transform.position;
            position.x = Random.Range(-11, 12);
            speed = Random.Range(10, speed);
            transform.position = position;

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        }

        void Update()
        {
            Vector3 position = transform.position;
            position.y = position.y - speed * Time.deltaTime;
            transform.position = position;
            
            Manager.Instance.CollectFruits(gameObject);

        }
    }

}
