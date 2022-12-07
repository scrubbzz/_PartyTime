using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StraightShootin
{
    /// <summary>
    /// Attached to the Arrow prefab object, and is active when a player instantiates it. Controls the aiming and firing
    /// of the arrow, given the variables passed in by said player.
    /// </summary>

    [RequireComponent(typeof(Rigidbody))]
    public class ArrowProjectile : MonoBehaviour
    {
        Rigidbody body;
        AudioSource audioSource;
        TrailRenderer trailRenderer;

        [HideInInspector] public PlayerAim player;

        public float arrowSpeedMultiplier = 50f;
        public float launchArcDegree = 25f;

        public LayerMask targetLayer = 10;
        public string targetTag;
        bool hasHitTarget;

        public float extraPitch;
        [SerializeField] AudioClip chargingSound;
        [SerializeField] AudioClip firingSound;

        void OnEnable()
        {
            body = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            trailRenderer = GetComponent<TrailRenderer>();

            /*if (player == null)
            {
                Debug.LogError("Arrow has no player reference. Make sure the object is being instantiated by a player.");
                Destroy(gameObject);
            }*/

        }

        public void SetUp(PassablePlayerAimData player)
        {
            Material mat = gameObject.GetComponentInChildren<Renderer>().material;
            mat.SetColor("Color", player.arrowColour);     
        }


        // called when player's reset timer completes, or player begins input
        public void ResetState(PassablePlayerAimData player)
        {
            // deactivate physics, stop the arrow
            body.useGravity = false;
            body.isKinematic = true;
            body.velocity = Vector3.zero;

            transform.position = player.defaultArrowPos;
            transform.rotation = player.defaultArrowRot;

            trailRenderer.enabled = false;
            audioSource.clip = chargingSound;

            hasHitTarget = false;
        }

        // called while the player holds the input down
        public void Aim(PassablePlayerAimData player)
        {
            transform.LookAt(player.hit.point);

            audioSource.pitch = (player.chargeValue / player.chargeLimit) + extraPitch; // get

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            
            // note: add charging sound effect.
        }

        // called when player releases input
        public void Fire(PassablePlayerAimData player)
        {
            if (player.chargeValue > 0)
            {
                // activate physics
                body.useGravity = true;
                body.isKinematic = false;

                // rotate upwards for a firing arc, throw in current direction
                transform.Rotate(Vector3.left, launchArcDegree);
                Vector3 forwardForce = transform.forward * player.chargeValue * arrowSpeedMultiplier;
                body.AddForce(forwardForce);

                trailRenderer.enabled = true;

                audioSource.clip = firingSound;
                audioSource.pitch = Random.Range(0.8f, 1.2f);
                audioSource.Play();
            }
        }



        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(targetTag))//layer == targetLayer)// && !hasHitTarget)
            {
                // pass the target's info over to player
                BreakableTarget target = collision.gameObject.GetComponent<BreakableTarget>();
                player.CheckTargetDamage(target);

                hasHitTarget = true; // make sure we don't accidentally break other targets
            }
        }

    }

}