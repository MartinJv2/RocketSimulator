using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PhysicEngine", menuName = "ScriptableObjects/PhysicEngine", order = 1)]
public class PhysicEngine : ScriptableObject
{
    public List<MotorProperty> motorlist = new List<MotorProperty>();
    public List<BaseProperty> objectlist = new List<BaseProperty>();
    public float weight = 0;
    public floatscriptableobject altitude;
    public float forcevisualize;
    private float _timesincestart;
    public floatscriptableobject speed;
    public UnityEvent OnPhysicUpdate;
    public boolvariable isrunning;
    private float _speed;
    public floatscriptableobject timevariable;

    public void OnEnable()
    {
        ResetVariable();
    }

    public void OnDisable()
    {
        ResetVariable();
    }

    public void ResetVariable()
    {
        weight = 0;
        altitude.value = 0;
        motorlist = new List<MotorProperty>();
        objectlist= new List<BaseProperty>();
        timevariable.value = 0;
        _timesincestart = 0;
        speed.value = 0;
        _speed = 0;
        isrunning.value = false;
    }
    public void RegisterMotor(GameObject motor)
    {
        RegisterObject(motor);
        motorlist.Add(motor.GetComponent<MotorProperty>());
    }
    public void RegisterObject (GameObject component)
    {
        objectlist.Add(component.GetComponent<BaseProperty>());
    }
    
    public void SimulateFrame(float time)
    {
        if (isrunning.value)
        {
            float acceleration = 0;
            float finalspeed = 0; 
            float initial_speed = _speed;
        
            _timesincestart += time;
            float force = TotalForceApplied();
            acceleration = force / weight;
            finalspeed = (acceleration * time) + initial_speed;
            altitude.value = (initial_speed + finalspeed) * _timesincestart/2;
            _speed = finalspeed;
            speed.value = _speed * 2;
            timevariable.value = _timesincestart;
            OnPhysicUpdate.Invoke();
        }
    }

    private float TotalForceApplied()
    {
        return CalculateGravityForce() + AddMotorForcenBaseOnTime();
    }
    private float AddMotorForcenBaseOnTime()
    {
        float force = 0;
        foreach (MotorProperty motor in motorlist)
        {
            if (_timesincestart <= motor.duration)
            {
                motor.GetComponent<ParticleSystem>().Play();
                force += motor.force * _timesincestart;
                motor.generatedtrusted = force;
            }
            else
            {
                if (motor.generatedtrusted < (motor.force * motor.duration))
                {
                    force += motor.force * motor.duration;
                    motor.generatedtrusted = force;
                }
                motor.GetComponent<ParticleSystem>().Stop(false,ParticleSystemStopBehavior.StopEmitting);
            }
        }
        return force;
    }
    public float CalculateGravityForce()
    {
        return (float)(-9.81 * weight);
    }

    public void RemoveMotor(GameObject gameobject)
    {
        motorlist.Remove(gameobject.GetComponent<MotorProperty>());
        RegisterObject(gameobject);
    }

    public void RemoveObject(GameObject gameobject)
    {
        objectlist.Remove(gameobject.GetComponent<BaseProperty>());
    }
}
