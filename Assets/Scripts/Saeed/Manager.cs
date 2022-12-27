using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FruitKahoot
{
    public class Manager : MonoBehaviour
    {

        public TextMeshProUGUI scoreText;
        private int score = 0;




        void Start()
        {

        }

        void Update()
        {

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

