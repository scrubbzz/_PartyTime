using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnowGlobalConflict
{
    [RequireComponent(typeof(AudioSource))] 
    public class FinishLineManager : MonoBehaviour
    {
        public GameObject finishedPlayer;
        public AudioSource audioSource;

        [SerializeField] private GameObject winScreen;
        void Start()
        {
            finishedPlayer = GameObject.FindGameObjectWithTag("Player");
            audioSource = this.GetComponent<AudioSource>(); 
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                /*finishedPlayer = other.gameObject;
                if (finishedPlayer != null)
                {
                    Debug.Log("FINISH LINE CROSSED");
                    finishedPlayer.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    finishedPlayer.GetComponent<PlayerMovement>().enabled = false;
                }*/
                Debug.Log("FINISH LINE CROSSED");
                finishedPlayer.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                finishedPlayer.GetComponent<PlayerMovement>().enabled = false;
                audioSource.PlayOneShot(audioSource.clip);
                winScreen.SetActive(true);
            }
        }
    }
}