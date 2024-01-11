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
    public string tutorialScene;
    public string starterscene;
    public List<string> tutorial;
    public int currentTutorial;
    public int maxTutorial;
    public TextMeshProUGUI element;
    
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
    public void next()
    {
        currentTutorial++;
        if (currentTutorial > tutorial.Count)
        {
            currentTutorial = 0;
        }
        element.text = tutorial[currentTutorial];
    }
    public void previous()
    {
        currentTutorial--;
        if (currentTutorial < 0)
        {
            currentTutorial =  tutorial.Count;
        }
        element.text = tutorial[currentTutorial];
    }
    public void instructionsScene()
    {
        SceneManager.LoadScene(tutorialScene, LoadSceneMode.Single);
        element.text = tutorial[currentTutorial];
    }
}
