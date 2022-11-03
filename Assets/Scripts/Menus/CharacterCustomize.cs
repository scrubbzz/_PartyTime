using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menus
{
    public class CharacterCustomize : MonoBehaviour
    {
        public GameObject boxerCustomize;
        public GameObject broccoliCustomize;
        public GameObject puncherCustomize;

        public Shop shop;

        private void Update()
        {
            ActivateCharacterButtons();
        }

        void ActivateCharacterButtons()
        {
            if (shop.hasBoxer == true) { boxerCustomize.SetActive(true); }
            if (shop.hasBroccoli == true) { broccoliCustomize.SetActive(true); }
            if (shop.hasPuncher == true) { puncherCustomize.SetActive(true); }
        }
    }
}

