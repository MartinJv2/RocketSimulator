using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donotdestroy : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
