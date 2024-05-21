using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTextForChallenge : MonoBehaviour
{
    public floatscriptableobject challengeStatusSpeed;
    public floatscriptableobject challengeStatusHeight;
    public GameObject HeightGoalOnScreen;
    public GameObject SpeedGoalOnScreen;
    public boolvariable Challenges;
    void Start()
    {
        if (Challenges.value)
        {
            if (challengeStatusSpeed.value != 0)
                    {
                        HeightGoalOnScreen.SetActive(false);
                    }
                    else if (challengeStatusHeight.value != 0)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
