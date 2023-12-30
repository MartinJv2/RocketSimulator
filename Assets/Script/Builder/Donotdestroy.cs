using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Donotdestroy : MonoBehaviour
{

    public List<string> exeptions;
    
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if(exeptions.Contains(currentScene))
        {
           Destroy(gameObject);
        }
    }
    
}
