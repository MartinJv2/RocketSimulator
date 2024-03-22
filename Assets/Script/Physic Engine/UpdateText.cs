using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UpdateText : MonoBehaviour
{
    public PhysicEngine physicengine;
    public TextMeshProUGUI textobject;
    public string prefix;
    public floatscriptableobject value;
    public string sufix;
    public void Start()
    {
        textobject = gameObject.GetComponent<TextMeshProUGUI>();
        physicengine.OnPhysicUpdate.AddListener(ChangeText);
    }

    public void ChangeText()
    {
        if (Mathf.Infinity == value.value)
        {
            textobject.text = prefix +"N/A "+ sufix;
        }
        else
        {
            textobject.text = prefix +Mathf.Round(value.value * 100f) / 100f + sufix;
        }
    }

    public void OnDestroy()
    {
        physicengine.OnPhysicUpdate.RemoveListener(ChangeText);
    }
}
