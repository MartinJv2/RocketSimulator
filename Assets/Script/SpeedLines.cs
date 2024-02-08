using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedLines : MonoBehaviour
{
    private ParticleSystem speedLines;
    public boolvariable isrunning;
  
    // Start is called before the first frame update
    void Start()
    {
        speedLines = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isrunning.value)
        {
            speedLines.Play();
        }
        else
        {
            speedLines.Stop();
        }
    }
}
