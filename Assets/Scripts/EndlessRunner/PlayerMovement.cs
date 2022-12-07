using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;

    public float acceleration = 10;
    public float maxSpeed = 10;
    [SerializeField] Rigidbody rb;
    Collider col;

    float horizontalInput;
    public float horizontalMultiplier = 2;

    [SerializeField] float jumpForce = 300f;
    public LayerMask groundMask;

    [SerializeField] string playerInput;
    [SerializeField] KeyCode jump;

    public Animator runnerAnimator;

    AudioSource jumpSound;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        jumpSound = GetComponent<AudioSource>(); 
    }

    private void FixedUpdate()
    {
        if (!alive) return;

        CalculateMovement();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw(playerInput) * horizontalMultiplier;

        if (Input.GetKeyDown(jump))
        {
            Jump();
            jumpSound.Play();
            runnerAnimator.SetTrigger("Jump");
        }

        if (transform.position.y < -5)
        {
            Die();
        }
    }

    void CalculateMovement()
    {

        Vector3 forwardMove = acceleration * Time.deltaTime * Vector3.forward;
        rb.AddForce(forwardMove, ForceMode.Impulse);

        Vector3 horizontalMove = horizontalInput * Time.deltaTime * Vector3.right;
        rb.AddForce(horizontalMove, ForceMode.Force);

        if (rb.velocity.magnitude > maxSpeed)
        {
            float speedLimitingForce = maxSpeed - rb.velocity.magnitude;
            rb.AddForce(rb.velocity.normalized * speedLimitingForce);
        }

        if (rb.velocity.x > - 0.05f || rb.velocity.x < 0.05f)
        {
            float oppositeForce = -rb.velocity.x * 2;
            rb.AddForce(new Vector3(oppositeForce, 0f));
        }

        //rb.velocity += Vector3.ClampMagnitude(forwardMove + horizontalMove, 20);
        //rb.MovePosition(rb.position + forwardMove + horizontalMove); 
    }

    /*bool ObstacleInWay()
    {
        float frontWidth = col.bounds.size.z / 2;

        return Physics.Raycast(transform.position, Vector3.forward, frontWidth + 0.3f, obstacleMask);
    }*/

    void Jump()
    {
        float height = col.bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    public void Die()
    {
        alive = false;

        Invoke("Restart", 2);

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
