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

    public override void Start()
    {
        weight = defaultweight;
        physicEngine.RegisterMotor(gameObject);
    }
}
