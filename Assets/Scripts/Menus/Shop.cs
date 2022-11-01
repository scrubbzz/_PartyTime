using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Menus
{
    public class Shop : MonoBehaviour
    {
        public int starAmount;

        public TMPro.TMP_Text starText;

        public bool hasBoxer = false;
        public bool hasPuncher = false;
        public bool hasBroccoli = false;

        public void BoxerGuy()
        {
            if (starAmount >= 800 && hasBoxer == false)
            {
                hasBoxer = true;
                starAmount = starAmount - 800;
                starText.text = "Super Stars : " + starAmount;

            }
        }

        public void PuncherGuy()
        {
            if (starAmount >= 1200 && hasPuncher == false)
            {
                hasPuncher = true;
                starAmount = starAmount - 1200;
                starText.text = "Super Stars : " + starAmount;
            }
        }

        public void BroccoliGuy()
        {
            if (starAmount >= 1200 && hasBroccoli == false)
            {
                hasBroccoli = true;
                starAmount = starAmount - 1200;
                starText.text = "Super Stars : " + starAmount;
            }
        }

        public void IncrementStarsBy1000()
        {
            starAmount = starAmount + 1000;
            starText.text = "Super Stars : " + starAmount;
        }

        public void IncrementStarsBy2800()
        {
            starAmount = starAmount + 2800;
            starText.text = "Super Stars : " + starAmount;
        }

        public void IncrementStarsBy5000()
        {
            starAmount = starAmount + 5000;
            starText.text = "Super Stars : " + starAmount;
        }

        public void IncrementStarsBy13500()
        {
            starAmount = starAmount + 13500;
            starText.text = "Super Stars : " + starAmount;
        }

    }
}

