using UnityEngine;

public class Coin : MonoBehaviour
{
   
    [SerializeField] LayerMask obstacleLayer;

    [SerializeField] GameObject coinFX; // child object to detach on collision.
    AudioSource coinAudio;
    ParticleSystem plusOneEffect; // particle system set to spawn one large particle with a "+1" texture.

    [SerializeField] float turnSpeed = 90f;


    private void Awake()
    {
        coinAudio = coinFX.GetComponent<AudioSource>();
        plusOneEffect = coinFX.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        // destroy this coin if intersecting with obstacles
        if (other.gameObject.layer == obstacleLayer)
        {
            Destroy(gameObject);
            return;
        }

        PlayerCoinCounter player = other.gameObject.GetComponent<PlayerCoinCounter>(); // cache player obj

        if (player != null) 
        { 
            // generate effects
            coinAudio.Play();
            plusOneEffect.Play();
            gameObject.transform.DetachChildren();

            player.IncrementScore(1);

            Destroy(gameObject, .11f);
            return;
        }

    }

}
