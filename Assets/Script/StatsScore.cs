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
    
    public void SaveData()
    {
        PlayerPrefs.SetFloat("Time", totalTime.value);
        PlayerPrefs.SetFloat("Height", totalHeight.value);
        PlayerPrefs.SetFloat("Actions", totalActions.value);
    }

    public void LoadData()
    {
        totalTime.value = PlayerPrefs.GetFloat("Time");
        totalHeight.value = PlayerPrefs.GetFloat("Height");
        totalActions.value = PlayerPrefs.GetFloat("Actions");
    }
}
