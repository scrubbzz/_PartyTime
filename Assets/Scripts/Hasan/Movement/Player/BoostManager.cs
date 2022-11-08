using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnowGlobalConflict
{
    public class BoostManager : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public GameObject boostTrail;
        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
            boostTrail = GameObject.Find("boost trail");
        }
        // Update is called once per frame
        void Update()
        {
            if (playerMovement.currentMoveSpeed > playerMovement.regularMoveSpeed)
            {
                playerMovement.movementSound.clip = Resources.Load<AudioClip>("SFX/fling");
                if (boostTrail != null)
                {
                    boostTrail.SetActive(true);
                }


            }
            else
            {
                playerMovement.movementSound.clip = Resources.Load<AudioClip>("SFX/RegularPlayerMovement");

                if (boostTrail != null)
                {

                    boostTrail.SetActive(false);
                }

            }
        }

    }

}
