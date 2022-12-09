using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnowGlobalConflict
{
    public class CoinsRotation : MonoBehaviour, IMoveable
    {
        public int rotSpeed;
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
            transform.Rotate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        public void ReadInputs()
        {

        }


    }

}
