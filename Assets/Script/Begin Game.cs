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
    public GameObject basicMenu;
    public Button CoinsButton;
    public Button SpeedButton;

    public void CoinLevels()
    {

        SpeedButton.GetComponent<Image>().color = new Color(.5f, .5f, .5f);
        CoinsButton.GetComponent<Image>().color = new Color(1f, 0.8352942f, 0.3647059f);
    }
    public void SpeedLevels()
    {
        SpeedButton.GetComponent<Image>().color = new Color(1f, 0.8352942f, 0.3647059f);
        CoinsButton.GetComponent<Image>().color = new Color(.5f, .5f, .5f);
    }
    public void BackToBeginingOfLevelsMenu()
    {
        basicMenu.SetActive(true);
        levels.SetActive(false);
    }
    public void MenuStart()
    {
     menu.SetActive(true);
     basicMenu.SetActive(false);
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
