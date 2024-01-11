using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static System.Linq.Enumerable;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public string tutorialScene;
    public List<string> tutorial;
    public int currentTutorial = 0;
    public TextMeshProUGUI element;
    
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
