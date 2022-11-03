using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallinFruits : MonoBehaviour
{
    public float speed = 0.1f;
    public Sprite[] sprites;
    void Start()
    {
        Vector3 position = transform.position;
        position.x = Random.Range(-11, 12);
        transform.position = position;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        
    }

    void Update()
    {
        Vector3 position = transform.position;
        position.y = position.y - speed;
        transform.position = position;

        Manager.Instance.Collect(gameObject);
        
    }
}
