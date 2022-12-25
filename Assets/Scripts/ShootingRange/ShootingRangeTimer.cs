using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace StraightShootin
{
    public class ShootingRangeTimer : Minigames.Generic.TimeControllableHandler
    {
        [SerializeField] float gameLength = 61f;
        [SerializeField] TextMeshProUGUI timerText;

        void Start()
        {
            timeRemaining = gameLength; // set value via inspector
            StartCoroutine(CountToMinigameStart());
        }

        IEnumerator CountToMinigameStart()
        {
            timerText.text = "Ready?";
            yield return new WaitForSeconds(1.5f);

            timerText.text = "Go!";
            yield return new WaitForSeconds(1.5f);

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

                    GameObject player = Minigames.Generic.PlayerSelector.GetRoundWinner();
                    timerText.text = "Time's up!\nThe winner is: " + player.name;
                }
            }
        }

        protected override void Countdown()
        {
            float time = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = "" + time;

            base.Countdown();
        }

        


    }
}