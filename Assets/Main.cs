using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadSceneAsync("level1");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
