using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTextForChallenge : MonoBehaviour
{
    public floatscriptableobject speedGoal;
    public floatscriptableobject heightGoal;
    public GameObject HeightGoalOnScreen;
    public GameObject SpeedGoalOnScreen;
    public boolvariable Challenges;
    public TextMeshProUGUI wonText;
    public TextMeshProUGUI loseText;
    public PhysicEngine physicEngine;
    public bool challengesDone = false;
    public StatsScore statsScore;
    public floatscriptableobject priceGoal;
    
    void Start()
    {
        if (Challenges.value)
        {
            if (speedGoal.value != 0)
            {
                HeightGoalOnScreen.SetActive(false);
            }

            if (heightGoal.value != 0)
            {
                SpeedGoalOnScreen.SetActive(false);
            }
        }
        else
        {
            HeightGoalOnScreen.SetActive(false);
            SpeedGoalOnScreen.SetActive(false);
        }

    
    }
    public void Won()
        {
            if (challengesDone == false)
            {
                 HeightGoalOnScreen.SetActive(false);
                 SpeedGoalOnScreen.SetActive(false);
                 wonText.gameObject.SetActive(true);
                 statsScore.SaveHighscore(heightGoal, speedGoal, priceGoal.value);
                 statsScore.LoadHighscores();
            }
        }
    public void Loss()
    {
        if (challengesDone == false)
        {
             HeightGoalOnScreen.SetActive(false);
             SpeedGoalOnScreen.SetActive(false);
             loseText.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (challengesDone == false)
        {
            if (physicEngine.challengeComplete)
            {
                Won();
                challengesDone = true;
            }
            if (physicEngine.challengeFailed)
            {
                Loss();
                challengesDone = true;
            }
        }
        
    }
}
