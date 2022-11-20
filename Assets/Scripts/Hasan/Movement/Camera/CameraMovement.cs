using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SnowGlobalConflict
{
    public class CameraMovement : MonoBehaviour
    {
        
        public Transform target;
        public Vector3 offset;

        public string playerName;
        public Quaternion rotation;
        // Start is called before the first frame update
        void Start()
        {
            //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            target = GameObject.Find(playerName).GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            FollowPlayer();
            //transform.rotation = Quaternion.identity;
            transform.rotation = rotation;
        }
        public void FollowPlayer()
        {
            transform.position = target.position + offset;
        }
    }
}
