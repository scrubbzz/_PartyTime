using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SnowGlobalConflict
{
    public class CameraMovement : MonoBehaviour, IAudioManageable
    {
        
        public Transform target;
        public Vector3 offset;

        public string playerName;
        public Quaternion rotation;

        [Header("Camera Effects")]
        public float shakeAmount;
        public event IAudioManageable.OnMovement onMovement;
        
        
        // Start is called before the first frame update
        void Start()
        {
            //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            target = GameObject.Find(playerName).GetComponent<Transform>();
            onMovement += PlaySoundEffect;
        }

        // Update is called once per frame
        void Update()
        {
            FollowPlayer();
            //transform.rotation = Quaternion.identity;
            transform.rotation = rotation;
        }
        public void FollowPlayer()
        {
            transform.position = target.position + offset;
        }

        public void PlaySoundEffect(AudioSource audioSource)
        {
            
        }
    }
}
