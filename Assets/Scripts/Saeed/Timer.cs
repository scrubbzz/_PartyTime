using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FruitKahoot
{
    public class Timer : MonoBehaviour
    {
        public GameObject player;
        public GameObject[] fruit;
        public GameObject[] bomb;
        public float delay;
        public float collectDistance;

        public GameObject gameover;
        private float timer;
        public float totalTime;
        public TextMeshProUGUI timerText;



        void Start()
        {
            timer = delay;


        }


        void Update()
        {
            timerText.text = "Timer:" + totalTime;


            if (totalTime <= 0)
            {
                totalTime = 0;
                player.GetComponent<plarr>().enabled = false;

            }

            totalTime -= Time.deltaTime;

            if (gameover.activeSelf)
            {
                return;
            }

            if (timer <= 0)
            {
                int i = Random.Range(0, fruit.Length);

                Instantiate(fruit[i], transform.position, Quaternion.identity);

                i = Random.Range(0, bomb.Length);
                Instantiate(bomb[i], transform.position, Quaternion.identity);
                timer = delay;
            }
            else
            {
                timer -= Time.deltaTime;
            }

        }
    }
}
