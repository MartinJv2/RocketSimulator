using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static System.Linq.Enumerable;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public List<string> tutorial;
    public int currentTutorial = 0;
    public TextMeshProUGUI element;

    private void Awake()
    {
        element.text = tutorial[currentTutorial];
    }

    public void next()
    {
        currentTutorial++;
        if (currentTutorial > tutorial.Count-1)
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
            currentTutorial =  tutorial.Count-1;
        }
        element.text = tutorial[currentTutorial];
    }
}
