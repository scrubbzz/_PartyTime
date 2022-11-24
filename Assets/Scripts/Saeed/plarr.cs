using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitKahoot
{
    public class plarr : MonoBehaviour
    {
        public float speed = 0.1f;
        public float xlimit = 11.9f;
        public Rigidbody rb;
        

        void Start()
        {
            rb = GetComponent<Rigidbody>();

        }


        void Update()
        {
            float input = Input.GetAxis("Horizontal");

            if (input != 0)
            {
                if (input > 0f)
                {
                    transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
                }
                else if (input < 0f)
                {
                    transform.GetChild(0).localScale = new Vector3(1, 1, 1);
                }
            }

            Vector3 position = transform.position;

          /*  rb.AddForce(new Vector3(input, 0, 0) * speed, ForceMode.Impulse);*/


            position.x = position.x + input * speed;

            if (position.x > xlimit)
            {
                position.x = xlimit;
            }
            else if (position.x < -xlimit)
            {
                position.x = -xlimit;

            }

            transform.position = position;
        }
    }

}
