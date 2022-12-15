using Menus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MakePlayerBuy : MonoBehaviour
{
    public Shop shop;

    public GameObject buyThis;

    public bool hasBought = false;

    public void HasNotBought()
    {
        if (shop.hasBoxer || shop.hasPuncher || shop.hasBroccoli == false)
        {
            hasBought= true;
            gameObject.SetActive(true);
        }
    }
    //public void HasBought(string sceneName)
    //{
    //    if (shop.hasBoxer || shop.hasPuncher || shop.hasBroccoli == true)
    //    {
    //        sceneLoader.LoadScene(sceneName);
    //    }
    //}
}
