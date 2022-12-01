using UnityEngine;

public class PlayerCoinCounter : MonoBehaviour
{
    int score;
    public TMPro.TMP_Text scoreText;


    public void IncrementScore(int valueToAdd)
    {
        score += valueToAdd;
        scoreText.text = "Score: " + score;
    }

    public void DecreaseScore(int valueToSubtract)
    {
        score -= valueToSubtract;

        if (score < 0) { score = 0; }

        scoreText.text = "Score: " + score;
    }
}
