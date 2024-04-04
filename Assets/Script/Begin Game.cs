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
    public GameObject menu;
    public GameObject levels;
    public Button CoinsButton;
    public Button SpeedButton;

    public void CoinLevels()
    {

        CoinsButton.GetComponent<Image>().color = new Color(.2f, .2f, .2f);
        SpeedButton.GetComponent<Image>().color = new Color(100, 100, 100);
    }
    public void SpeedLevels()
    {
        SpeedButton.GetComponent<Image>().color = new Color(0f, 0f, 0f);
        CoinsButton.GetComponent<Image>().color = new Color(.5f, .5f, .5f);
    }
    public void BackToBeginingOfLevelsMenu()
    {
        gameObject.SetActive(true);
        levels.SetActive(false);
    }
    public void MenuStart()
    {
     menu.SetActive(true);
     gameObject.SetActive(false);
    }
    public void SeeLevels()
    {
        menu.SetActive(false);
        levels.SetActive(true);
    }
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
