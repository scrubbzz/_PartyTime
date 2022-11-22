using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class IScrewedUp : MonoBehaviour
{
    public float multiplier = 0.1f;
    public bool go = false;
    // Start is called before the first frame update
    void Go()
    {
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.transform.parent == null)
            {
                gameObject.transform.localScale *= multiplier;
                gameObject.transform.position *= multiplier;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (go)
        {
            Go();
            go = false;
            print("done");
        }
    }
}
