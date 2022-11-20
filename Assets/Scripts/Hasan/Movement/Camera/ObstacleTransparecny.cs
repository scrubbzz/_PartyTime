using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnowGlobalConflict
{

    public class ObstacleTransparecny : MonoBehaviour
    {
        private CameraMovement cameraMovement;
        public LayerMask obstacleLayerMask;

        private List<Collider> transparentColliders = new List<Collider>();
        // Start is called before the first frame update
        void Start()
        {
            cameraMovement = GetComponent<CameraMovement>();
            Debug.Log(cameraMovement.target.gameObject.name);
        }

        // Update is called once per frame
        void Update()
        {
            SeeThroughObjects();
            //RestoreObjects();
        }
        private void SeeThroughObjects()
        {
            Ray ray = new Ray(transform.position, (cameraMovement.target.position - transform.position).normalized);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                Debug.Log("Now Transparent");

                if (hit.transform != cameraMovement.target)
                {
                    Material material = hit.collider.GetComponent<Renderer>().material;
                    material.color = new Color(material.color.r, material.color.g, material.color.b, 0.2f);
                    transparentColliders.Add(hit.collider);
                    Debug.Log("You cant see");
                }
                else
                {
                    RestoreObjects();
                }
            }
            
            /*  if (!Physics.Raycast(ray, out hit, obstacleLayerMask))
              {
                  hitObjectMaterial = hit.collider.GetComponent<Material>().color.a;
                  hitObjectMaterial = 1;
              }
  */
        }
        private void RestoreObjects()
        {
            for (int i = 0; i < transparentColliders.Count; i++)
            {
                Material colMaterial = transparentColliders[i].GetComponent<Renderer>().material;
                colMaterial.color = new Color(colMaterial.color.r, colMaterial.color.g, colMaterial.color.b, 1f);
                Debug.Log("No Longer Transparent");
                transparentColliders.RemoveAt(i);
            }

        }
    }
}
