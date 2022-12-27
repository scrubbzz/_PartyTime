using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class DataCollector : MonoBehaviour
{
    public static float seconds;

    Scene scene;
    private void Start()
    {
        scene = SceneManager.GetActiveScene();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        seconds += Time.deltaTime;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode load)
    {
        Collection();
    }
    void Collection()
    {
        string path = Application.dataPath + "/Log.txt";

        DateTime dt = DateTime.Now;
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Login log \n\n");
        }

        if (scene.buildIndex == 1)
        {
            File.AppendAllText(path, "Main Menu \n\n");
        }

        if (scene.buildIndex == 2)
        {
            File.AppendAllText(path, "Snow Global Conflict \n\n");
        }

        if (scene.buildIndex == 3)
        {
            File.AppendAllText(path, "Straight Shootin \n\n");
        }

        if (scene.buildIndex == 4)
        {
            File.AppendAllText(path, "Fun Run \n\n");
        }

        string date = "Login Date: " + dt + "\n" + "Time Played: " + timeSpan + "\n";
        File.AppendAllText(path, date);
    }

    private void OnApplicationQuit()
    {
        Collection();
    }
    
}
