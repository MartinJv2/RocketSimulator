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
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if(exeptions.Any(sceneName.Contains))
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
