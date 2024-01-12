using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleButton : MonoBehaviour
{
    public boolvariable isruning;
    public TextMeshProUGUI text;
    public string runtext;
    public string stoptext;

    public void Toggle()
    {
        if (isruning.value)
        {
            isruning.value = false;
            text.text = runtext;

        }
        else
        {
            isruning.value = true;
            text.text = stoptext;
        }
    }
}
