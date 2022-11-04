using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnowGlobalConflict
{
    [RequireComponent(typeof(Rigidbody))]
    public class Obstacle : MonoBehaviour
    {
        [SerializeField]
        Rigidbody rb;

        public float rotSpeed;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            rb.angularVelocity += Vector3.up * Random.Range(2, 20) * Time.deltaTime * rotSpeed;
            rb.velocity = Vector3.zero;
        }
    }
}
