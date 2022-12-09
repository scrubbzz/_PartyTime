using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnowGlobalConflict
{
    public class BoostManager : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public TrailRenderer boostTrail;
        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
            //boostTrail = GameObject.Find("boost trail");
            boostTrail = GetComponentInChildren<TrailRenderer>();
        }
        // Update is called once per frame
        void Update()
        {
            if (playerMovement.currentMoveForce > playerMovement.regularMoveForce)
            {
                playerMovement.movementSound.clip = Resources.Load<AudioClip>("SFX/fling");
                if (boostTrail != null)
                {
                    //boostTrail.SetActive(true);
                    boostTrail.enabled = true;
                }


            }
            else
            {
                playerMovement.movementSound.clip = Resources.Load<AudioClip>("SFX/RegularPlayerMovement");

                if (boostTrail != null)
                {

                    boostTrail.enabled = false;
                }

            }
        }

    }

}
