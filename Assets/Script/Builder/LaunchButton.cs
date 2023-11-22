using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchButton : MonoBehaviour
{
    public void Onlaunch(string scenename)
    {
        SceneManager.LoadScene(scenename, LoadSceneMode.Single);
    }
}
