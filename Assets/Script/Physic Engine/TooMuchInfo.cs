using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooMuchInfo : MonoBehaviour
{
    private int cycleIndex = 0;
    public int cycleMax;
    public List<GameObject> infoCycling;
    int index = 0;

    public void InfoCycle()
    {
        if (cycleIndex != cycleMax)
        {
            cycleIndex++;
        }
        else
        {
            cycleIndex = 0;
        }
        foreach (GameObject info in infoCycling)
        {
            if (index < cycleIndex)
            {
                info.SetActive(false);
            }
            else
            {
                info.SetActive(true);
            }

            index++;
        }
        index = 0;
    }
}
