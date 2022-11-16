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

        public LayerMask targetLayer = 6;
        bool hasHitTarget;


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

            hasHitTarget = false;
        }

        // called while the player holds the input down
        public void Aim(PassablePlayerAimData player)
        {
            transform.LookAt(player.hit.point);
            // note: add charging sound effect.
        }

        // called when player releases input
        public void Fire(PassablePlayerAimData player)
        {
            // activate physics
            body.useGravity = true;
            body.isKinematic = false;

            // rotate upwards for a firing arc, throw in current direction
            transform.Rotate(Vector3.left, launchArcDegree);
            Vector3 forwardForce = transform.forward * player.chargeValue * arrowSpeedMultiplier;
            body.AddForce(forwardForce);

            trailRenderer.enabled = true;
            //trailRenderer.time = forwardForce.z / 50;

            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();
        }



        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == targetLayer && !hasHitTarget)
            {
                // pass the target's info over to player
                ShooterTarget target = collision.gameObject.GetComponent<ShooterTarget>();
                player.CheckTargetDamage(target);

                hasHitTarget = true; // make sure we don't accidentally break other targets
            }
        }

    }

}