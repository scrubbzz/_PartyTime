using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitKahoot
{
    public class Platforms : MonoBehaviour
    {
        public float minX = 2f;
        public float maxX = 3f;
        void Start()
        {
            minX = transform.position.x;
            maxX = transform.position.x + 3;
        }


        void Update()
        {
            transform.position = new Vector3(Mathf.PingPong(Time.time * 3, maxX - minX) + minX, transform.position.y, transform.position.z);
        }

    }

}
