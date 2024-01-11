using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static System.Linq.Enumerable;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginGame : MonoBehaviour
{
    public string builderscene;
    public string starterscene;
    public string tutorialScene;
    
    public void Begin()
    {
        SceneManager.LoadScene(builderscene, LoadSceneMode.Single);
    }
    public void Back()
    {
        SceneManager.LoadScene(starterscene, LoadSceneMode.Single);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void instructionsScene()
    {
        SceneManager.LoadScene(tutorialScene, LoadSceneMode.Single);
    }
}
