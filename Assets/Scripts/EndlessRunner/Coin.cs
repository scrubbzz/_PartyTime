using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;

    AudioSource coinAudio;

    private void Awake()
    {
        coinAudio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" || other.gameObject.name == "Player2") { coinAudio.Play(); }

        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.name != "Player" || other.gameObject.name != "Player2")
        {
            return;
        }

        GameManager.inst.IncrementScore();

        Destroy(gameObject,.11f);
    }

}
