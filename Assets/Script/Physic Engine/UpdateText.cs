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
        textobject.text = prefix + value.value + sufix;
    }

    public void OnDestroy()
    {
        physicengine.OnPhysicUpdate.RemoveListener(ChangeText);
    }
}
