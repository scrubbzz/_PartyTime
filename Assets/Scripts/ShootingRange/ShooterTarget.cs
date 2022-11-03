using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTarget : MonoBehaviour 
{
    Vector3 direction = Vector3.zero;
    public float moveSpeed = 1;

    public float damageTotalRequired = 20;
    public float damageAllowance = 10; // value representing leeway the player has to not perfectly hit the damage required value and still break the target. 
                                // the higher the leeway, the easier it is for the player to successfully break the target

    public int valueInCoins;

    float timeActive;

    Renderer targetRenderer;

    GameObject targetParticleObj;
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

        targetParticleObj = transform.GetChild(0).gameObject;
        particles = targetParticleObj.GetComponent<ParticleSystem>();
        if (particles == null)
        {
            Debug.LogError("you need to make sure there's one child object with a ParticlesSystem component on your target object.");
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
                transform.DetachChildren();
            }

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
                transform.DetachChildren();
            }

            Destroy(gameObject);
            Debug.Log("target was hit too hard");
            return 0;
        }
        else 
        {
            Debug.Log("target wasn't hit hard enough");
            return 0;
        }
        
    }


}
