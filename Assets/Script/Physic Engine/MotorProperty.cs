using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MotorProperty : BaseProperty
{
    public float force;
    public float duration;
    public float generatedtrusted;
    private bool isanimationruning;
    public bool isrunning = true;
    public boolvariable gamerunning;
    public float stoptime;

    public override void Start()
    {
        weight = defaultweight;
        physicEngine.RegisterMotor(gameObject);
    }

    public void OnMouseDown()
    {
        if (gamerunning.value)
        {
            isrunning = !isrunning;
            if (isrunning && generatedtrusted < force*duration){startanimation();}
            else{stopanimation();}
        }
    }

    public void startanimation()
    {
        if (!isanimationruning)
        {
            isanimationruning = true;
            GetComponent<ParticleSystem>().Play();
        }
    }

    public void stopanimation()
    {
        if (isanimationruning)
        {
            isanimationruning = false;
            GetComponent<ParticleSystem>().Stop(false,ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
