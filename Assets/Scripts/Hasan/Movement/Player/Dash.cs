using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour, IMoveable
{
    public float dashSpeed;
    public bool canDash;

    [SerializeField]
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        ReadInputs();
    }
    public void Move(float moveSpeed)
    {
        if(rb.velocity != Vector3.zero)
        {
            transform.forward = rb.velocity;
        }
        rb.position = Vector3.MoveTowards(rb.position, rb.position + new Vector3(0, 0, -moveSpeed * 10), dashSpeed);
        rb.velocity = Vector3.zero;
    }

    public void ReadInputs()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Move(dashSpeed);
        }
        
    }

}
