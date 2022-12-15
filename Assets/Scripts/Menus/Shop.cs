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
        public bool thanks = false;

        public GameObject boxerButton;
        public GameObject broccoliButton;   
        public GameObject puncherButton;
        public GameObject dealButton;
        public GameObject sold1;
        public GameObject sold2;
        public GameObject sold3;
        public GameObject buyMore;

        public AudioSource caChing;

        public GameObject inSuffFunds;

        private void Start()
        {
            Time.timeScale = 1.0f;
        }
        public void BoxerGuy()
        {
            if (starAmount >= 800 && hasBoxer == false)
            {
                hasBoxer = true;
                starAmount = starAmount - 800;
                starText.text = "Party Dollars : " + starAmount;
                boxerButton.SetActive(false);
                sold1.SetActive(true);

            }
            else { inSuffFunds.SetActive(true); }
        }

        public void PuncherGuy()
        {
            if (starAmount >= 1200 && hasPuncher == false)
            {
                hasPuncher = true;
                starAmount = starAmount - 1200;
                starText.text = "Party Dollars : " + starAmount;
                puncherButton.SetActive(false);
                sold2.SetActive(true);
            }
            else { inSuffFunds.SetActive(true); }
        }

        public void BroccoliGuy()
        {
            if (starAmount >= 1200 && hasBroccoli == false)
            {
                hasBroccoli = true;
                starAmount = starAmount - 1200;
                starText.text = "Party Dollars : " + starAmount;
                broccoliButton.SetActive(false);
                sold3.SetActive(true);
            }
            else { inSuffFunds.SetActive(true); }
        }

        public void IncrementStarsBy1000()
        {
            starAmount = starAmount + 1000;
            starText.text = "Party Dollars : " + starAmount;
            caChing.Play();
        }

        public void IncrementStarsBy2800()
        {
            starAmount = starAmount + 2800;
            starText.text = "Party Dollars : " + starAmount;
            caChing.Play();
        }

        public void IncrementStarsBy5000()
        {
            starAmount = starAmount + 5000;
            starText.text = "Party Dollars : " + starAmount;
            caChing.Play();
        }

        public void IncrementStarsBy13500()
        {
            starAmount = starAmount + 13500;
            starText.text = "Party Dollars : " + starAmount;
            caChing.Play();
        }


    }
}

