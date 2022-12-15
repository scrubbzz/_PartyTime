using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataCollector : MonoBehaviour
{
    void Collection()
    {
        string path = Application.dataPath + "/Log.txt";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Login log \n\n");
        }

        string date = "Login Date: " + System.DateTime.Now + "\n";
        File.AppendAllText(path, date);
    }

    private void Start()
    {
        Collection();
    }
}
