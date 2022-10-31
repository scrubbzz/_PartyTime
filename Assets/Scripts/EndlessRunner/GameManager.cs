using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;

    public static GameManager inst;

    public TMPro.TMP_Text scoreText;

    private void Awake()
    {
        inst = this;
    }

    public void IncrementScore()
    {
        if (GameObject.FindGameObjectWithTag("Coin"))
        {
            score++;
            scoreText.text = "Score: " + score;
        }
    }

    public void IncrementScoreBox()
    {
        if (GameObject.FindGameObjectWithTag("CoinBox"))
        {
            score = score + 10;
            scoreText.text = "Score: " + score;
        }
    }

    public void DecreaseScoreGrass()
    {
        int temp = score;
        if (GameObject.FindGameObjectWithTag("Grass") && score >= 10 )
        {
            score = score - 10;
            scoreText.text = "Score: " + score;
        }
        else 
        { 
            score = score - temp;
            scoreText.text = "Score: " + score; 
        }
    }
}
