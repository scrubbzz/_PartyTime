using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StraightShootin
{
    /// <summary>
    /// Attached to objects meant to be shot at. Handles damage calculation and effect displays when hit, 
    /// and follows a select movement pattern (yet to be implemented).
    /// </summary>
    public class ShooterTarget : MonoBehaviour
    {
        Vector3 direction = Vector3.zero;
        public float moveSpeed = 1;

        public float damageTotalRequired = 20;
        public float damageAllowance = 10; // value representing leeway the player has to not perfectly hit the damage required value and still break the target. 
                                           // the higher the leeway, the easier it is for the player to successfully break the target

        public int valueInCoins;

        public AudioClip successBreakSound;
        public AudioClip failBreakSound;
        public AudioClip noBreakSound;

        float timeActive;

        Renderer targetRenderer;

        AudioSource audioSource; // to do: implement audio interface.

        GameObject targetEffectsObj;
        ParticleSystem particles;

        private void OnEnable()
        {
            if (Camera.main.WorldToViewportPoint(transform.position).x > 0.5) // if spawned on right side
            {
                this.direction = Vector3.left; // move left
            }
            else if (Camera.main.WorldToViewportPoint(transform.position).x <= 0.5) // if spawned on left side
            {
                this.direction = Vector3.right; // move right
            }

            targetRenderer = GetComponent<Renderer>();

            targetEffectsObj = transform.GetChild(0).gameObject;
            particles = targetEffectsObj.GetComponent<ParticleSystem>();
            if (particles == null)
            {
                Debug.LogError("you need to make sure the first child object on your target object has a ParticleSystem component.");
            }
            audioSource = targetEffectsObj.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("you need to make sure the first child object on your target object has an AudioSource component.");
            }
        }

        private void Update()
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
            timeActive += Time.deltaTime;

            if (!targetRenderer.isVisible && timeActive > 1)
            {
                Destroy(gameObject);
                //Debug.Log("despawned");
            }
        }


        public int CalculateDamageTaken(float damageValue, PlayerAim player)
        {
            // award points when in the green zone for damage
            if (damageValue > damageTotalRequired - damageAllowance && damageValue < damageTotalRequired + damageAllowance)
            {
                // play breaking animation
                if (particles != null)
                {
                    particles.startColor = Color.yellow;
                    particles.Play();
                }

                audioSource.clip = successBreakSound;
                audioSource.Play();

                transform.DetachChildren();

                Destroy(gameObject);
                Debug.Log("you got " + valueInCoins + " coins!");
                return valueInCoins;

            }
            else if (damageValue > damageTotalRequired + damageAllowance) // destroy when over the damage limit
            {
                if (particles != null)
                {
                    particles.startColor = Color.black;
                    particles.Play();
                }

                audioSource.clip = failBreakSound;
                audioSource.Play();

                transform.DetachChildren();

                Destroy(gameObject);
                Debug.Log("target was hit too hard");
                return 0;
            }
            else
            {
                audioSource.clip = noBreakSound;
                audioSource.Play();

                Debug.Log("target wasn't hit hard enough");
                return 0;
            }

        }


    }
}
