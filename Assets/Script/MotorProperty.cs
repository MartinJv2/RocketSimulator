using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MotorProperty : BaseProperty
{
    public float force;
    [FormerlySerializedAs("ignitetime")] public float outOfFuel;
}
