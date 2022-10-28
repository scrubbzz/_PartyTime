using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowGlobeCoins : Coins
{
    public int coinCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CollectCoin()
    {
       coinCount++;
       coinCountUI.text = "Coins = " + coinCount.ToString();
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            CollectCoin();
            Destroy(other.gameObject, 0.2f);
        }
    }
}
