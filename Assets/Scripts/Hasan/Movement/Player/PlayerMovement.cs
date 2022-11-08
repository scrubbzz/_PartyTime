using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnowGlobalConflict
{

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour, IMoveable, IAudioManageable
    {

        [Header("Speed")]
        public float currentMoveSpeed;
        public int speedLimit;
        private float verticalSpeed;
        private float horizontalSpeed;

        public float increaseRate;
        public float decreaseRate;

        public float resetRate;
        public float regularMoveSpeed;

        private bool isMoving;

        public float delayTime;
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
            regularMoveSpeed = 9;
            currentMoveSpeed = regularMoveSpeed;
            
            movementSound = GetComponent<AudioSource>();
            onMovement += PlaySoundEffect;
        }

        // Update is called once per frame
        void Update()
        {
            ReadInputs();
            Move(currentMoveSpeed);

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

        public void Move(float moveSpeed)
        {
            horizontalDirection = new Vector3(horizontalSpeed, 0, 0);
            verticalDirection = new Vector3(0, 0, verticalSpeed);
            
            rb.AddForce((horizontalDirection + verticalDirection) * moveSpeed);
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
            if (currentMoveSpeed > regularMoveSpeed)
            {
                currentMoveSpeed -= Time.deltaTime * resetRate;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Pad")
            {
                currentMoveSpeed += regularMoveSpeed;
            }
        }
    }
}
