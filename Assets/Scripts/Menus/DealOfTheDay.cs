using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Menus
{
    public class DealOfTheDay : MonoBehaviour
    {
        Shop shop;
        public GameObject dealButton;
        void Start()
        {
            if(shop.hasBoxer == true) { dealButton.SetActive(false); }
        }

    }
}

