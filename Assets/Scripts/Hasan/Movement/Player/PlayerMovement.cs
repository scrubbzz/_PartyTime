using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnowGlobalConflict
{

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour, IMoveable, IAudioManageable
    {

        [Header("Speed")]
        public float currentMoveForce;
        public int speedLimit;
        private float verticalSpeed;
        private float horizontalSpeed;

        public float increaseRate;
        public float decreaseRate;

        public float resetRate;
        public float regularMoveForce;

        private bool isMoving;

        private float delayTime;
        [Header("Direction")]
        private Vector3 horizontalDirection;
        private Vector3 verticalDirection;
        private Rigidbody rb;

        [Header("Audio")]

        public AudioSource movementSound;

        [Header("AxisControl")]
        public string horizontalName;
        public string verticalName;

        public event IAudioManageable.OnMovement onMovement;

       
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            //regularMoveSpeed = 3;
            currentMoveForce = regularMoveForce;
            resetRate = regularMoveForce * 2; 
            movementSound = GetComponent<AudioSource>();
            onMovement += PlaySoundEffect;
        }

        // Update is called once per frame
        void Update()
        {
            ReadInputs();
            Move(currentMoveForce);

            if (isMoving)
            {
                onMovement(movementSound);
            }
            ResetSpeed();
        }
        public void ReadInputs()
        {
            if (Input.GetAxis(horizontalName) != 0 && horizontalSpeed < speedLimit)
            {
                Debug.Log("You moving");
                isMoving = true;
                //horizontalSpeed += Time.deltaTime * increaseRate;
                horizontalSpeed = Input.GetAxis(horizontalName) * increaseRate;
            }
            if (Input.GetAxis(verticalName) != 0 && verticalSpeed < speedLimit)
            {
                isMoving = true;
                //verticalSpeed += Time.deltaTime * increaseRate;
                verticalSpeed = Input.GetAxis(verticalName) * increaseRate;
            }

            if (Input.GetAxis(horizontalName) == 0 && Input.GetAxis(verticalName) == 0)
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
            if (verticalSpeed > 0 && !isMoving)
            {
                verticalSpeed -= Time.deltaTime * decreaseRate;
            }
            if (verticalSpeed < 0 && !isMoving)
            {
                verticalSpeed += Time.deltaTime * decreaseRate;
            }
        }

        public void Move(float moveForce)
        {
            horizontalDirection = new Vector3(horizontalSpeed, 0, 0);
            verticalDirection = new Vector3(0, 0, verticalSpeed);

            /* if (isMoving)
             {
                 transform.forward = horizontalDirection + verticalDirection; 
             }
             */
            //rb.velocity = (horizontalDirection + verticalDirection) * moveSpeed;
           
                rb.AddForce((horizontalDirection + verticalDirection) * moveForce, ForceMode.Impulse);
            Vector3 rbVelocity = rb.velocity;
            if(rbVelocity.x > speedLimit)
            {
                rbVelocity.x = speedLimit;
            }
            if(rbVelocity.z > speedLimit)
            {
                rbVelocity.z = speedLimit;
            }
            
           /* var x = rb.velocity.x;
            var z = rb.velocity.z;*/
            //rb.velocity = new Vector3(Mathf.Clamp(x, -horizontalSpeed * moveSpeed, horizontalSpeed * moveSpeed), 0, Mathf.Clamp(z, -verticalSpeed * moveSpeed, verticalSpeed * moveSpeed));
        }

        public void PlaySoundEffect(AudioSource audioSource)
        {
            delayTime += Time.deltaTime;
            if (delayTime > audioSource.clip.length)
            {
                audioSource.PlayOneShot(audioSource.clip);
                delayTime = 0;
            }

        }
        public void ResetSpeed()
        {
            if (currentMoveForce > regularMoveForce)
            {
                currentMoveForce -= Time.deltaTime * resetRate;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Pad")
            {
                if (currentMoveForce < speedLimit && currentMoveForce == Mathf.Clamp(currentMoveForce, regularMoveForce - 1, regularMoveForce + 1))
                {
                    currentMoveForce += /*regularMoveSpeed*/ regularMoveForce * 5;
                }
            }
        }
    }
}
