using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;
    
    [SerializeField] float speed = 5;
    [SerializeField] Rigidbody rb;

    float horizontalInput;
    public float horizontalMultiplier = 2;

    [SerializeField] float jumpForce = 300f;
    public LayerMask groundMask;

    [SerializeField] string playerInput;
    [SerializeField] KeyCode jump;

    public Animator runnerAnimator;


    private void FixedUpdate()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove); 
    }

    void Update()
    {
        horizontalInput = Input.GetAxis(playerInput);

        if (Input.GetKeyDown(jump))
        {
            Jump();
            runnerAnimator.SetTrigger("Jump");
        }

        if (transform.position.y < -5)
        {
            Die();
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

    void Jump()
    {
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }      
    }
}
