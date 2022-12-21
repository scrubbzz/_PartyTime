using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FruitKahoot
{
    public class Manager : MonoBehaviour
    {
        public GameObject[] fruit;
        public GameObject[] bomb;
        public Transform playerTransform;
        public float delay;
        public float collectDistance;

        public GameObject gameover;
        public TextMeshProUGUI scoreText;
        private float timer;
        private int score = 0;


        public static Manager Instance;
        private void Awake()
        {
            Instance = this;

        }
        void Start()
        {


            timer = delay;

        }


        void Update()
        {
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

        public void CollectFruits(GameObject fruit)
        {


            score = score + 1;

            scoreText.text = "Coins: " + score.ToString();
            Destroy(fruit);

        }

        public void CollectBombs(GameObject Bombs)
        {
            score = score - 5;

            if (score <= 0)
            {
                score = 0;
            }

            scoreText.text = "Coins: " + score.ToString();
            Destroy(Bombs);

        }
    }

}

