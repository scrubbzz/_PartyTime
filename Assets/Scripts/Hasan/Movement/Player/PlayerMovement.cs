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
        public float regularSpeed;

        private bool isMoving;

        public float delayTime;
        [Header("Direction")]
        private Vector3 horizontalDirection;
        private Vector3 verticalDirection;
        private Rigidbody rb;

        [Header("Audio")]

        public AudioSource movementSound;

        public event IAudioManageable.OnMovement onMovement;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            currentMoveSpeed = 9;
            //horizontalDirection = new Vector3(moveSpeed, 0, 0);
            //verticalDirection = new Vector3(0, 0, moveSpeed);
            onMovement += PlaySoundEffect;
        }

        // Update is called once per frame
        void Update()
        {
            ReadInputs();
            Move(currentMoveSpeed);

            /* if (Input.GetKeyDown(KeyCode.P))
             {
                 onMovement(movementSound);
             }*/
            if (isMoving)
            {

                /* float soundDelay = 0;
                 soundDelay += Time.deltaTime;

                 if (soundDelay > 2)
                 {
                     movementSound.Play();
                     Debug.Log("You Suck " + movementSound.clip.name);
                     soundDelay = 0;
                 }*/


                onMovement(movementSound);


            }
            ResetSpeed();
        }
        public void ReadInputs()
        {
            if (Input.GetAxis("Horizontal") != 0 && horizontalSpeed < speedLimit)
            {
                isMoving = true;
                //horizontalSpeed += Time.deltaTime * increaseRate;
                horizontalSpeed = Input.GetAxis("Horizontal") * increaseRate;
            }
            if (Input.GetAxis("Vertical") != 0 && verticalSpeed < speedLimit)
            {
                isMoving = true;
                //verticalSpeed += Time.deltaTime * increaseRate;
                verticalSpeed = Input.GetAxis("Vertical") * increaseRate;
            }

            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
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
            /* if (rb.velocity.x < 30 && rb.velocity.z < 30)
             {*/

            rb.AddForce((horizontalDirection + verticalDirection) * moveSpeed);
            //}
            //rb.velocity = (horizontalDirection + verticalDirection) * moveSpeed;

        }

        public void PlaySoundEffect(AudioSource audioSource)
        {
            delayTime += Time.deltaTime;
            if (delayTime > audioSource.clip.length)
            {
                audioSource.PlayOneShot(audioSource.clip);
                delayTime = 0;
            }

            //audioSource.PlayScheduled(audioSource.clip.length);
            //StartCoroutine(PlaySound(audioSource));

        }
        public void ResetSpeed()
        {
            if (currentMoveSpeed > regularSpeed)
            {
                currentMoveSpeed -= Time.deltaTime * resetRate;

            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Pad")
            {


                currentMoveSpeed += regularSpeed;

            }
        }
        /*private void OnCollisionStay(Collision collision)
        {
            if(collision.gameObject.tag == "Pad")
            {
                moveSpeed += 30;
            }
        }*/

        /* private void OnCollisionExit(Collision collision)
         {
             if(collision.gameObject.tag == "Pad")
             {
                 moveSpeed -= 30;
             }
         }*/
    }
}
