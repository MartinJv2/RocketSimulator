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
    public boolvariable Challenges;
    public TMP_Dropdown SelectedPrice;
    public TMP_Dropdown SelectedHeight;
    public TMP_Dropdown SelectedTime;
    public TMP_Dropdown SelectedPrice_speed;
    public StatsScore statsScore;
    public floatscriptableobject totalTime;
    

    public void Update()
    {
        totalTime.value += Time.deltaTime;
        statsScore.SaveData();
    }

    public void SetHeight()
    {
        if (SelectedHeight.value % 2f == 1)
        {
            HeightGoal.value = 1000*(float)(Math.Pow(10, SelectedHeight.value/2));
        }
        else if (SelectedHeight.value%2f == 0)
        {
            HeightGoal.value = 500*(float)(Math.Pow(10, SelectedHeight.value/2));
        }
    }
    public void SetPrice_speed()
    {
        if (SelectedPrice_speed.value % 2f == 1)
        {
            PriceGoal.value = 1000000*(float)(Math.Pow(10, SelectedPrice_speed.value/2));
        }
        else if (SelectedPrice_speed.value%2f == 0)
        {
            PriceGoal.value = 500000*(float)(Math.Pow(10, SelectedPrice_speed.value/2));
        }
    }
    public void SetSpeed()
    {
        if (SelectedTime.value % 2f == 1)
        {
            SpeedGoal.value = 1000*(float)(Math.Pow(10, SelectedTime.value/2));
        }
        else if (SelectedTime.value%2f == 0)
        {
            SpeedGoal.value = 500*(float)(Math.Pow(10, SelectedTime.value/2));
        }
    }
    public void ChallengesCancelled()
    {
        Challenges.value = false;
    }
    public void SetPrice()
    {
        if (SelectedPrice.value % 2f == 1)
        {
            PriceGoal.value = 1000000*(float)(Math.Pow(10, SelectedPrice.value/2));
        }
        else if (SelectedPrice.value%2f == 0)
        {
            PriceGoal.value = 500000*(float)(Math.Pow(10, SelectedPrice.value/2));
        }
    }
    public void CoinLevels()
    {
        coinChallenges.SetActive(true);
        speedChallenges.SetActive(false);
        SpeedButton.GetComponent<Image>().color = Color.white;
        CoinsButton.GetComponent<Image>().color = new Color(1f, 0.8352942f, 0.3647059f);
        if (SelectedHeight.value % 2f == 1)
        {
            HeightGoal.value = 1000*(float)(Math.Pow(10, SelectedHeight.value/2));
        }
        else if (SelectedHeight.value%2f == 0)
        {
            HeightGoal.value = 500*(float)(Math.Pow(10, SelectedHeight.value/2));
        }
        
        if (SelectedPrice.value % 2f == 1)
        {
            PriceGoal.value = 1000000*(float)(Math.Pow(10, SelectedPrice.value/2));
        }
        else if (SelectedPrice.value%2f == 0)
        {
            PriceGoal.value = 500000*(float)(Math.Pow(10, SelectedPrice.value/2));
        }
        SpeedGoal.value = 0;
    }
    public void SpeedLevels()
    {
        speedChallenges.SetActive(true);
        coinChallenges.SetActive(false);
        SpeedButton.GetComponent<Image>().color = new Color(1f, 0.8352942f, 0.3647059f);
        CoinsButton.GetComponent<Image>().color = Color.white;
        if (SelectedPrice_speed.value % 2f == 1)
        {
            PriceGoal.value = 1000000*(float)(Math.Pow(10, SelectedPrice_speed.value/2));
        }
        else if (SelectedPrice_speed.value%2f == 0)
        {
            PriceGoal.value = 500000*(float)(Math.Pow(10, SelectedPrice_speed.value/2));
        }
        
        if (SelectedTime.value % 2f == 1)
        {
            SpeedGoal.value = 1000*(float)(Math.Pow(10, SelectedTime.value/2));
        }
        else if (SelectedTime.value%2f == 0)
        {
            SpeedGoal.value = 500*(float)(Math.Pow(10, SelectedTime.value/2));
        }

        HeightGoal.value = 0;
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
        Challenges.value = true;
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
        statsScore.SaveData();
        Application.Quit();
    }
    public void instructionsScene()
    {
        SceneManager.LoadScene(tutorialScene, LoadSceneMode.Single);
    }
}
