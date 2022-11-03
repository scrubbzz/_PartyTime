using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnowGlobalConflict
{
    /// <summary>
    /// Attatched to the player. Player runs into coins, 'collects' them, then a sound effect plays.
    /// </summary>
    public class SnowGlobeCoins : Coins, IAudioManageable
    {
        public int coinCount;

        [SerializeField]
        private AudioSource coinSource;
        public event IAudioManageable.OnMovement onMovement;
        // Start is called before the first frame update
        void Start()
        {
            onMovement += PlaySoundEffect;
        }

        // Update is called once per frame
        void Update()
        {
        }
        public override void CollectCoin()
        {
            coinCount++;
            coinCountUI.text = "Coins: " + coinCount.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Coin")
            {
                CollectCoin();
                onMovement(coinSource);
                Destroy(other.gameObject, 0.2f);
            }
        }

        public void PlaySoundEffect(AudioSource audioSource)
        {
            audioSource.PlayOneShot(audioSource.clip);
            Debug.Log("You collected a coin");
        }
    }

}
