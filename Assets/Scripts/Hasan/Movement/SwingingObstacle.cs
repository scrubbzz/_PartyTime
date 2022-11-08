using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwingingObstacle : MonoBehaviour, IMoveable
{
    public float rotSpeed;
    public Vector3 rotationEulers;

    public float minX;
    public float maxX;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move(rotSpeed);
    }
    public void Move(float moveSpeed)
    {
        float xVlaue = Mathf.Sin(Time.time);
        float rotAmount = xVlaue * moveSpeed;
        float xRotation = Mathf.Clamp(xVlaue, minX, maxX);
        //transform.Rotate(new Vector3(xRotation, 0, 0));
       

            transform.Rotate(new Vector3(rotAmount * Time.deltaTime, 0, 0));
       

        /* Quaternion rot = transform.rotation;
         rot.x += maxX * Mathf.Sin(Time.time * moveSpeed);*/
    }

    public void ReadInputs()
    {
        throw new System.NotImplementedException();
    }

}
