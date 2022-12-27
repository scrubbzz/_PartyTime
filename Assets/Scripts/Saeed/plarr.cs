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
        public GameObject moveParticles;
        public Animator fruitPlayerAnim;
        public float jumpForce = 2f;
        public float gravityvalue;
        public bool isGrounded;
        public string movementInput;
        public KeyCode jumpKey;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            Physics.gravity = new Vector3(0, gravityvalue, 0);

        }


        void Update()
        {
            float input = Input.GetAxis(movementInput);

            if (input != 0)
            {
                moveParticles.SetActive(true);
                fruitPlayerAnim.SetBool("Run", true);

                if (input > 0f)
                {
                    transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
                }
                else if (input < 0f)
                {
                    transform.GetChild(0).localScale = new Vector3(1, 1, 1);
                }
            }

            if (input == 0)
            {
                StartCoroutine(ParticleTimer());
                fruitPlayerAnim.SetBool("Run", false);
                fruitPlayerAnim.SetBool("Idle", true);
            }

            Vector3 position = transform.position;

            if (Input.GetKeyDown(jumpKey) && isGrounded)
            {
                rb.AddForce(new Vector3(0, jumpForce, 0) * speed, ForceMode.Impulse);
                isGrounded = false;

            }

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

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Plane")
            {
                isGrounded = true;
            }
        }

        IEnumerator ParticleTimer()
        {
            yield return new WaitForSeconds(0.2f);
            moveParticles.SetActive(false);
        }
    }

}
