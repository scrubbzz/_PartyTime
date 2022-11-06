using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnowGlobalConflict
{
    public class BoostManager : MonoBehaviour
    {
        public PlayerMovement playerMovement;
       
        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
        // Update is called once per frame
        void Update()
        {
            if (playerMovement.currentMoveSpeed > playerMovement.regularMoveSpeed)
            {
                playerMovement.movementSound.clip = Resources.Load<AudioClip>("SFX/fling");
               
            }
            else
            {
                playerMovement.movementSound.clip = Resources.Load<AudioClip>("SFX/RegularPlayerMovement");
            }
        }
        
    }

}
