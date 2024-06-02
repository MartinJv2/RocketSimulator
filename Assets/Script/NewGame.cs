using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    public StatsScore statsScore;
    public floatscriptableobject timeSpent;
    
    void Start()
    {
        if (timeSpent.value == 0)
        {
            statsScore.LoadData();
        }
        
    }
    
}
