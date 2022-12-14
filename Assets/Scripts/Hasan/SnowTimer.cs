using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SnowGlobalConflict
{

    public class SnowTimer : Timer
    {
        [SerializeField]
        private float gameTimer;

        [SerializeField]
        private Text timerText;
        [SerializeField]
        private TextMeshProUGUI timerTextGUI;

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
            timerTextGUI.text = "Time Left: " + gameTimer.ToString();
        }
        public override void TimeGame(float gameTimerLength)
        {
            gameTimer = gameTimerLength;
            gameTimer -= Time.deltaTime;
            Debug.Log("The time is " + gameTimerLength);
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
