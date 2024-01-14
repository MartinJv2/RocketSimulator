using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public floatscriptableobject gravityacelleration;

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
            altitude.value += (initial_speed+finalspeed)/2 * time;
            _speed = finalspeed;
            speed.value = _speed;
            timevariable.value = _timesincestart;
            OnPhysicUpdate.Invoke();
        }
    }

    private float TotalForceApplied()
    {
        return CalculateGravityForce() + AddMotorForceBaseOnTime() + CalculateDragForce();
    }
    private float AddMotorForceBaseOnTime()
    {
        float force = 0;
        foreach (MotorProperty motor in motorlist)
        {
            if (motor == null)
            {
                continue;
            }
            if (_timesincestart <= motor.duration)
            {
                motor.GetComponent<ParticleSystem>().Play();
                force += motor.force * motor.duration;
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
        double EarthMass = 5.972 * Math.Pow(10, 24);
        double UniversalGravitationalContant = 6.674 * Math.Pow(10, -11);
        double EarthRadius = 6.371 * Math.Pow(10, 6);
        double distance = EarthRadius + Convert.ToDouble(altitude.value);
        gravityacelleration.value = -(float)((UniversalGravitationalContant * EarthMass) /
                                            (Math.Pow(distance, 2)));
        return  (float)(gravityacelleration.value * weight);
    }

    public float CalculateDragForce()
    {
        float cd = CalculateCd();
        float airdensity = CalculateAirDensity();
        float radius = CalculateRadius(); 
        float referencearea = (float)(Math.PI * Math.Pow(radius, 2));
        if (speed.value >= 0)
        {
            return -(float)(cd*referencearea* airdensity* Math.Pow(speed.value, 2) /2);
        }
        return (float)(cd*referencearea* airdensity* Math.Pow(speed.value, 2) /2);
    }

    public float CalculateCd()
    {
        return (float)(0.0112 * 63.434948 + 0.162);
    }
    public float CalculateRadius()
    {
        return 0.5f;
    }
    public float CalculateAirDensity()
    {
        if ((288.15 - 0.0065 * altitude.value) == 0)
        {
            altitude.value += 1;
        }
        return (float)(352.995*(Math.Pow(1 - 0.000022557*altitude.value, 5.25516))/(288.15 - 0.0065*altitude.value));
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
