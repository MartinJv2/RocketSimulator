using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

[CreateAssetMenu(fileName = "BestScore", menuName = "ScriptableObjects/BestScore", order = 1)]
public class StatsScore : ScriptableObject
{
    public floatscriptableobject totalTime;
    public floatscriptableobject totalActions;
    public floatscriptableobject totalHeight;
    public floatscriptableobject heightSOO;
    public floatscriptableobject heightIOOO;
    public floatscriptableobject heightSOOO;
    public floatscriptableobject heightIOOOO;
    public floatscriptableobject heightSOOOO;
    public floatscriptableobject heightIOOOOO;
    
    public floatscriptableobject speedIOO;
    public floatscriptableobject speedSOO;
    public floatscriptableobject speedIOOO;
    
    public void SaveData()
    {
        PlayerPrefs.SetFloat("Time", totalTime.value);
        PlayerPrefs.SetFloat("Height", totalHeight.value);
        PlayerPrefs.SetFloat("Actions", totalActions.value);
    }

    public void LoadHighscores()
    {
        heightSOO.value = PlayerPrefs.GetFloat("500");
        heightIOOO.value = PlayerPrefs.GetFloat("1000");
        heightSOOO.value = PlayerPrefs.GetFloat("5000");
        heightIOOOO.value = PlayerPrefs.GetFloat("10000");
        heightSOOOO.value = PlayerPrefs.GetFloat("50000");
        heightIOOOOO.value = PlayerPrefs.GetFloat("100000");

        speedIOO.value = PlayerPrefs.GetFloat("100");
        speedSOO.value = PlayerPrefs.GetFloat("500");
        speedIOOO.value = PlayerPrefs.GetFloat("1000");
       
    }

    public void LoadData()
    {
        totalTime.value = PlayerPrefs.GetFloat("Time");
        totalHeight.value = PlayerPrefs.GetFloat("Height");
        totalActions.value = PlayerPrefs.GetFloat("Actions");
    }

    public void SaveHighscore(floatscriptableobject heightgoal, floatscriptableobject speedgoal, float record)
    {
        if (heightgoal.value != 0)
        {
            if (PlayerPrefs.GetFloat(heightgoal.value.ToString()) == 0)
            {
                PlayerPrefs.SetFloat(heightgoal.value.ToString(), record);
            }
            else if (PlayerPrefs.GetFloat(heightgoal.value.ToString()) > record)
            {
                PlayerPrefs.SetFloat(heightgoal.value.ToString(), record);
            }
        }
        else
        {
            if (PlayerPrefs.GetFloat(speedgoal.value.ToString()) == 0)
            {
                PlayerPrefs.SetFloat(speedgoal.value.ToString(), record);
            }
            else if (PlayerPrefs.GetFloat(speedgoal.value.ToString()) > record)
            {
                PlayerPrefs.SetFloat(speedgoal.value.ToString(), record);
            }
        }
        
    } 
}
