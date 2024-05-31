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
    private float bestscore;
    public string filepath;
    public List<float> stats = new List<float>();
    public floatscriptableobject totalTime;
    public floatscriptableobject totalActions;
    public floatscriptableobject totalHeight;


    public void Begin()
    {
        totalTime.value = stats[0];
        totalHeight.value = stats[1];
        totalActions.value = stats[2];
    }

    public void UpdateStatus()
    {
        stats[0] = totalTime.value;
        stats[1] = totalHeight.value;
        stats[2] = totalActions.value;
    }
    
    
}
