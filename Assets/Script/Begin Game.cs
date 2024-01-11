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
    public List<string> tutorial;
    private int currentTutorial;
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
    public void Tutoriel()
    {
       if (currentTutorial == maxTutorial)
       {
           currentTutorial = 0;
       }
       element.text = tutorial[currentTutorial];
    }
    
}
