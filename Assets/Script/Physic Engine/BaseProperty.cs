using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProperty : MonoBehaviour
{
    
    public PhysicEngine physicEngine;
    private float _weight = 0;
    public float defaultweight;
    public float last_x = 1;
    public float last_z = 1;
    public float last_y = 1;
    public float decalement_x = 0;
    public float decalement_z = 0;
    public float decalement_y = 0;
    public float last_x_rotation = 1;
    public float last_y_rotation = 1;
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
