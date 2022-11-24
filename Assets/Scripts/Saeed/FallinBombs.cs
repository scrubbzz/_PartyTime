using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitKahoot
{
    public class FallinBombs : MonoBehaviour
    {
        public float speed;
        public Sprite[] sprites;
        void Start()
        {
            Vector3 position = transform.position;
            position.x = Random.Range(-9, 10);
            speed = Random.Range(6, 12);
            transform.position = position;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        }

        void Update()
        {
            Vector3 position = transform.position;
            position.y = position.y - speed * Time.deltaTime;
            transform.position = position;

            Manager.Instance.CollectBombs(gameObject);

        }
    }
}


