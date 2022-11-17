using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Menus
{
    public class DealOfTheDay : MonoBehaviour
    {
        public Shop shop;
        public GameObject dealButton;
        void Update()
        {
            if(shop.hasBoxer == true) { dealButton.SetActive(false); }
        }

    }
}

