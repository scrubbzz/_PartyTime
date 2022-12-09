using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitKahoot
{
    public class Movements : MonoBehaviour
    {
        public float currentMoveSpeed;
        private float horizontalSpeed;
        public int speedLimit;
        public float regularMoveSpeed;

        public float increaseRate;
        public float decreaseRate;

        private Vector3 horizontalDirection;
        private bool isMoving;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            regularMoveSpeed = 3;
            currentMoveSpeed = regularMoveSpeed;

        }

        // Update is called once per frame
        void Update()
        {
            ReadInputs();
            Move(currentMoveSpeed);

        }

        public void ReadInputs()
        {
            if (Input.GetAxis("Horizontal") != 0 && horizontalSpeed < speedLimit)
            {
                Debug.Log("You moving");
                isMoving = true;
                //horizontalSpeed += Time.deltaTime * increaseRate;
                horizontalSpeed = Input.GetAxis("Horizontal") * increaseRate;
            }

            if (Input.GetAxis("Horizontal") == 0)
            {
                isMoving = false;
            }

            UpdateMoveSpeed();
        }


        private void UpdateMoveSpeed()
        {
            if (horizontalSpeed > 0 && !isMoving)
            {
                horizontalSpeed -= Time.deltaTime * decreaseRate;
            }
            if (horizontalSpeed < 0 && !isMoving)
            {
                horizontalSpeed += Time.deltaTime * decreaseRate;
            }

        }

        public void Move(float moveSpeed)
        {
            horizontalDirection = new Vector3(horizontalSpeed, 0, 0);
            
            rb.AddForce((horizontalDirection) * moveSpeed);
        }

    }
}



