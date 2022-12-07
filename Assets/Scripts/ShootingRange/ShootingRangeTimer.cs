using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace StraightShootin
{
    public class ShootingRangeTimer : Minigames.Generic.TimerControllableHandler
    {
        [SerializeField] float gameLength = 61f;
        [SerializeField] TextMeshProUGUI timerText;

        void Start()
        {
            timeRemaining = gameLength;
            StartCoroutine(CountToMinigameStart());
        }

        IEnumerator CountToMinigameStart()
        {
            timerText.text = "Ready?";
            yield return new WaitForSeconds(1);

            timerText.text = "Go!";
            yield return new WaitForSeconds(1);

            tickingActive = true;
            StartObjects();
        }

        void Update()
        {
            if (tickingActive)
            {
                Countdown();

                if (timeRemaining <= 0)
                {
                    StopObjects(); // have the winner determine script subscribe to this.
                    tickingActive = false;
                    timerText.text = "Time's up!";
                }
            }
        }

        protected override void Countdown()
        {
            float time = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = "" + time;

            base.Countdown();
        }

        

        void SelectWinnersShooter()
        {
            // to do: transfer this to a function with "winner selector" script.

            GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
            PlayerAim[] players = new PlayerAim[playerObjs.Length];

            for (int i = 0; i < playerObjs.Length; i++)
            {
                players[i] = playerObjs[i].GetComponent<PlayerAim>();
            }

            GameObject player = null;
            int coins = 0;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].coinCounter > coins)
                {
                    coins = players[i].coinCounter;
                    player = players[i].gameObject;
                }
            }

            //testTimer.text = "Time's up! \nWinner is: " + player.name;
        }


        


    }
}