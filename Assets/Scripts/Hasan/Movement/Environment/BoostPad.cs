using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
    [SerializeField]
    private float boostSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var rb = collision.gameObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                var rbVelocity = rb.velocity.z;
                rbVelocity *= boostSpeed;
                //Debug.Log("you moron");
            }
        }
    }
}
