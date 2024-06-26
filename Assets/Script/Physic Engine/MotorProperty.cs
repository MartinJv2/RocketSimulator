using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MotorProperty : BaseProperty
{
    public float force;
    public float duration;
    public Vector3 generatedtrusted = Vector3.zero;
    private bool isanimationruning;
    public bool isrunning = true;
    public boolvariable gamerunning;
    public float stoptime;
    public float anglebetweenvectorandy;
    public float anglebetweenprojectvectorandx;
    

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
            if (isrunning && IsBurnComplete()){startanimation();}
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

    public bool IsBurnComplete()
    {
        Vector3 vector =
            physicEngine.CreateVectorBaseOnLenghtAndLenght(force, anglebetweenvectorandy,
                anglebetweenprojectvectorandx);
        return vector.x * duration < generatedtrusted.x &&
               vector.y * duration < generatedtrusted.y &&
               vector.z * duration < generatedtrusted.z;
    }
}
