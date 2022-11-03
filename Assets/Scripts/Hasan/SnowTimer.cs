using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SnowGlobalConflict
{

    public class SnowTimer : Timer
    {
        [SerializeField]
        private float gameTimer;

        [SerializeField]
        private Text timerText;

        [SerializeField]
        private GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            TimeGame(gameTimer);
            timerText.text = "Time Left: " + gameTimer.ToString();
        }
        public override void TimeGame(float gameTimerLength)
        {
            gameTimer = gameTimerLength;
            gameTimer -= Time.deltaTime;
            //gameTimer = Mathf.RoundToInt(gameTimer);

            if(gameTimer <= 0)
            {
                gameTimer = 0;
                player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                player.GetComponent<PlayerMovement>().enabled = false;
            }
            
        }

    }

}
