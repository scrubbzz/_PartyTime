using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnowGlobalConflict
{

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour, IMoveable, IAudioManageable
    {

        [Header("Speed")]
        public float moveSpeed;
        public int speedLimit;
        public float verticalSpeed;
        public float horizontalSpeed;

        public float increaseRate;
        public float decreaseRate;

        public bool isMoving;

        [Header("Direction")]
        public Vector3 horizontalDirection;
        public Vector3 verticalDirection;
        public Rigidbody rb;

        [Header("Audio")]
        [SerializeField]
        private AudioSource movementSound;

        public event IAudioManageable.OnMovement onMovement;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            //horizontalDirection = new Vector3(moveSpeed, 0, 0);
            //verticalDirection = new Vector3(0, 0, moveSpeed);
            onMovement += PlaySoundEffect;
        }

        // Update is called once per frame
        void Update()
        {
            ReadInputs();
            Move(moveSpeed);

            if (Input.GetKeyDown(KeyCode.P))
            {
                onMovement(movementSound);
            }
           
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

            //rb.AddForce((horizontalDirection + verticalDirection) * moveSpeed);
            rb.velocity = (horizontalDirection + verticalDirection) * moveSpeed;

        }

        public void PlaySoundEffect(AudioSource audioSource)
        {
            audioSource.PlayOneShot(audioSource.clip);
            //StartCoroutine(PlaySound(audioSource));
           
            Debug.Log("You Suck" + audioSource.clip.name);
        }

    }
}
