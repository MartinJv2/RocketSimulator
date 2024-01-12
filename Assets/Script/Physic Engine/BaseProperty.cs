using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProperty : MonoBehaviour
{
    
    public PhysicEngine physicEngine;
    private float _weight = 0;
    public float defaultweight;
    public float weight
    {
        get { return _weight;}
        set
        {
            physicEngine.weight += value - _weight;
            _weight = value;
        }
    }

    public virtual void Start()
    {
        weight = defaultweight;
        physicEngine.RegisterObject(gameObject);
    }
}
