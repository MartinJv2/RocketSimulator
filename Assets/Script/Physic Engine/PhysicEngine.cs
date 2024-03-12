using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    private Vector3 _speed;
    public floatscriptableobject timevariable;
    public floatscriptableobject gravityacelleration;
    public floatscriptableobject dragforce;
    public floatscriptableobject currentorientation;
    public Vector3 position;
    public float angletolerance;

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
        _speed = new Vector3(0 ,0 ,0);
        isrunning.value = false;
        currentorientation.value = 0;
        position = new Vector3();
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
            Vector3 acceleration;
            Vector3 finalspeed; 
            Vector3 initial_speed = _speed;
        
            _timesincestart += time;
            Vector3 force = TotalForceApplied();
            acceleration = force / weight;
            finalspeed = (acceleration * time) + initial_speed;
            position += (initial_speed+finalspeed)/2 * time;
            altitude.value = position.y;
            _speed = finalspeed;
            if (_speed.y >= 0)
            {
                speed.value = _speed.magnitude;
            }
            else
            {
                speed.value = -_speed.magnitude;
            }
            timevariable.value = _timesincestart;
            OnPhysicUpdate.Invoke();
            currentorientation.value = FindOrienatation();
        }    
    }

    private float FindOrienatation()
    {
        if (_speed.x == 0)
        {
            return 0f;
        }
        else
        {
            float neworientation = (float)(Math.Atan(Math.Abs(_speed.x / _speed.y)) * 180 / Math.PI);
            if (Math.Abs(neworientation - currentorientation.value) < angletolerance)
            {
                return currentorientation.value;
            }
            else
            {
                return neworientation;
            }
        }
    }

    private float ConvertDegreeToRad(double angle)
    {
        return (float)(angle * Math.PI/180);
    }
    private Vector3 TotalForceApplied()
    {
        Vector3 value = new Vector3(0, CalculateGravityForce(),0);
        value += CalculateDragForce();
        value += AddMotorForceBaseOnTime();
        return  value;
    }

    private Vector3 CreateVector3fromlenghtandorientation(float lenght)
    {
        float orientation = ConvertDegreeToRad(currentorientation.value);
        return new Vector3((float)(Math.Sin(orientation) * lenght), (float)Math.Cos(orientation) * lenght, 0);
    }
    private Vector3 AddMotorForceBaseOnTime()
    {
        float force = 0;
        foreach (MotorProperty motor in motorlist)
        {
            if (motor == null)
            {
                Debug.Log("motor == null");
                continue;
            }
            if (_timesincestart <= motor.duration)
            {
                if (!motor.isanimationruning)
                {
                    motor.GetComponent<ParticleSystem>().Play();
                    motor.isanimationruning = true;
                }
                force += motor.force / motor.duration;
                motor.generatedtrusted = force;
            }
            else
            {
                if (motor.generatedtrusted < (motor.force * motor.duration))
                {
                    force += motor.force * motor.duration;
                    motor.generatedtrusted = force;
                }

                if (motor.isanimationruning)
                {
                    motor.isanimationruning = false;
                    motor.GetComponent<ParticleSystem>().Stop(false,ParticleSystemStopBehavior.StopEmitting);
                }
            }
        }
        return CreateVector3fromlenghtandorientation(force);
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

    private Vector3 CalculateDragForce()
    {
        return new Vector3(CalculateDragForce(_speed.x),CalculateDragForce(_speed.y),CalculateDragForce(_speed.z));
    }

    private float CalculateDragForce(float speed)
    {
        float force = 0;
        float cd = CalculateCd();
        float airdensity = CalculateAirDensity();
        float radius = CalculateRadius(); 
        float referencearea = (float)(Math.PI * Math.Pow(radius, 2));
        if (speed >= 0)
        {
            force = -(float)(cd*referencearea* airdensity* Math.Pow(speed, 2) /2);
        }
        else
        {
            force = (float)(cd*referencearea* airdensity* Math.Pow(speed, 2) /2);
        }
        return force;
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

        if ((1 - 0.000022557 * altitude.value) <= 0)
        {
            return 0f;
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
