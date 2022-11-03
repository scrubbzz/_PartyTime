using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public GameObject prefab;
    public Transform playerTransform;
    public float delay = 0.5f;
    public float collectDistance = 2;

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
            Instantiate(prefab, transform.position, Quaternion.identity);
            timer = delay;
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }
    
    public void Collect(GameObject fruit)
    {
        if(Vector2.Distance(playerTransform.position, fruit.transform.position) < collectDistance)
        {
            score = score + 1;

            scoreText.text = "Score: " + score.ToString();
            Destroy(fruit);
        }
    }
}
