using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArrowProjectile : MonoBehaviour
{
    Rigidbody body;
    AudioSource audioSource;
    [HideInInspector] public PlayerAim player;

    public float arrowSpeedMultiplier = 50f;
    public float launchArcDegree = 25f;

    public Vector3 defaultArrowPos;
    public Quaternion defaultArrowRot;

    public LayerMask targetLayer = 6;
    bool hasHitTarget;


    void OnEnable()
    {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        ResetState();
    }

    public void ResetState()
    {
        // deactivate physics, stop the arrow
        body.useGravity = false;
        body.isKinematic = true;
        body.velocity = Vector3.zero; 

        transform.position = defaultArrowPos; // reset the arrow's transform
        transform.rotation = defaultArrowRot;

        hasHitTarget = false;
    }

    public void Aim(Vector3 aimTo)
    {
        // change direction
        transform.LookAt(aimTo);
    }

    public void Fire(float powerToFlingWith)
    {
        // activate physics
        body.useGravity = true;
        body.isKinematic = false;

        // rotate upwards for a firing arc, throw in current direction
        transform.Rotate(Vector3.left, launchArcDegree);
        body.AddForce(transform.forward * powerToFlingWith * arrowSpeedMultiplier);

        audioSource.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        // check if the collision obj is a target
        if (collision.gameObject.layer == targetLayer && !hasHitTarget)
        {
            ShooterTarget target = collision.gameObject.GetComponent<ShooterTarget>();

            player.CheckTargetDamage(target); // pass target info to player

            hasHitTarget = true; // make sure we don't accidentally break other targets
        }
    }

}
