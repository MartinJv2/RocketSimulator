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
    public GameObject demoRocket;
    public GameObject levels;
    public GameObject basicMenu;
    public Button CoinsButton;
    public Button SpeedButton;
    public GameObject coinChallenges;
    public GameObject speedChallenges;
    public floatscriptableobject PriceGoal;
    public floatscriptableobject HeightGoal;
    public floatscriptableobject SpeedGoal;
    public TMP_Dropdown SelectedPrice;
    public TMP_Dropdown SelectedHeight;
    public TMP_Dropdown SelectedTime;
    public TMP_Dropdown SelectedPrice_speed;

    public void SetPrice()
    {
        if (SelectedPrice.value % 2f == 1)
        {
            PriceGoal.value = 1000*(float)(Math.Pow(10, SelectedPrice.value/2));
        }
        else if (SelectedPrice.value%2f == 0)
        {
            PriceGoal.value = 500*(float)(Math.Pow(10, SelectedPrice.value/2));
        }
        print(PriceGoal.value);
    }
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
        demoRocket.SetActive(true);
    }
    public void MenuStart()
    {
     menu.SetActive(true);
     basicMenu.SetActive(false);
     demoRocket.SetActive(false);
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
