using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using static System.Linq.Enumerable;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FolderScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject acheivments;
    public Button LevelsBeatenButton;
    public Button StatsButton;
    public GameObject LevelsBeaten;
    public GameObject Stats;
    
    public void LevelsBeat()
    {
        LevelsBeaten.SetActive(true);
        Stats.SetActive(false);
        StatsButton.GetComponent<Image>().color = Color.white;
        LevelsBeatenButton.GetComponent<Image>().color = new Color(1f, 0.8352942f, 0.3647059f);
        
    }
    public void Statistics()
    {
        Stats.SetActive(true);
        LevelsBeaten.SetActive(false);
        StatsButton.GetComponent<Image>().color = new Color(1f, 0.8352942f, 0.3647059f);
        LevelsBeatenButton.GetComponent<Image>().color = Color.white;
    }
    
    public void SeeAcheivments()
    {
        Stats.SetActive(false);
        LevelsBeaten.SetActive(false);
        menu.SetActive(false);
        acheivments.SetActive(true);
    }
}
