using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;
    
    [SerializeField] float speed = 5;
    [SerializeField] Rigidbody rb;
    //[SerializeField] Rigidbody rb2;

    float horizontalInput;
    //float horizontalInput2p;
    public float horizontalMultiplier = 2;

    [SerializeField] float jumpForce = 300f;
    public LayerMask groundMask;

    //[SerializeField] bool isSecondPlayer;

    private void FixedUpdate()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        //Vector3 horizontalMove2 = transform.right * horizontalInput2p * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
        //rb2.MovePosition(rb2.position + forwardMove + horizontalMove2);
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //horizontalInput2p = Input.GetAxis("Horizontal2");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Jump();
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
