using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
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
    public GameObject coinChallenges;
    public GameObject speedChallenges;
    public floatscriptableobject PriceGoal;
    public Dropdown SelectedPrice;
    public Dropdown SelectedHeight;

    public void CoinLevels()
    {
        coinChallenges.SetActive(true);
        speedChallenges.SetActive(false);
        SpeedButton.GetComponent<Image>().color = Color.white;
        CoinsButton.GetComponent<Image>().color = new Color(1f, 0.8352942f, 0.3647059f);
    }
    public void SpeedLevels()
    {
        speedChallenges.SetActive(true);
        coinChallenges.SetActive(false);
        SpeedButton.GetComponent<Image>().color = new Color(1f, 0.8352942f, 0.3647059f);
        CoinsButton.GetComponent<Image>().color = Color.white;
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
        speedChallenges.SetActive(false);
        coinChallenges.SetActive(false);
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
