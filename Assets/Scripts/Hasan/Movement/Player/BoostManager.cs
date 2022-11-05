using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnowGlobalConflict
{
    public class BoostManager : PlayerMovement
    {
        
       
      

        // Update is called once per frame
        void Update()
        {
            if (currentMoveSpeed > regularSpeed)
            {
                movementSound.clip = Resources.Load<AudioClip>("SpeedBoostSound");
               
            }
            else
            {
                movementSound.clip = Resources.Load<AudioClip>("RegularMovementSound");
            }
        }
        
    }

}
