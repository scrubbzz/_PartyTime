using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMSceneLoader : MonoBehaviour
{
    public Animator wave;
    public Animator trans;

    public void LoadScene()
    {
        StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public IEnumerator Load(int levelIndex)
    {
        wave.SetTrigger("Wave");
        trans.SetTrigger("Start");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(levelIndex);
    }
}
