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
    private Vector3 AddMotorForceBaseOnTime()
    {
        Vector3 force = Vector3.zero;
        foreach (MotorProperty motor in motorlist)
        {
            if (motor == null)
            {
                Debug.Log("motor == null");
                continue;
            }

            if (!motor.isrunning)
            {
                motor.stoptime += Time.deltaTime;
            }
            else if (_timesincestart - motor.stoptime<= motor.duration && motor.isrunning)
            {
                motor.startanimation();
                force += CreateVectorBaseOnLenghtAndLenght(motor.force, motor.anglebetweenvectorandy, motor.anglebetweenprojectvectorandx )/ motor.duration;
                motor.generatedtrusted = force;
            }
            else
            {
                if (motor.IsBurnComplete())
                {
                    force += CreateVectorBaseOnLenghtAndLenght(motor.force, motor.anglebetweenvectorandy, motor.anglebetweenprojectvectorandx )* motor.duration;
                    motor.generatedtrusted = force;
                }
                motor.stopanimation();
                motor.isrunning = false;
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

    private Vector3 CalculateDragForce()
    {
        Vector3 result = new Vector3(CalculateDragForce(_speed.x), CalculateDragForce(_speed.y),
            CalculateDragForce(_speed.z));

        dragforce.value = result.magnitude;
        return result;
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
        return (float)((28 * CalculateAirPressure()) / (8.31*CalculateAirTemperature()));
    }

    public float CalculateAirTemperature()
    {
        float lb;
        float tb;
        float hb;
        
        if (altitude.value < 11000)
        {
            hb = 0;
            lb = -6.5f;
            tb = 15;
            return ((altitude.value - hb) * lb + tb) +273.15f;
        }
        else if (altitude.value < 20000)
        {
            hb = 11000;
            lb = 0;
            tb = (float)-97.5;
            return ((altitude.value - hb) * lb + tb) +273.15f;
        }
        else if (altitude.value<32000)
        {
            hb = 20000;
            lb = 1;
            tb = (float)-97.5;
            return ((altitude.value - hb) * lb + tb) +273.15f;
        }
        else if (altitude.value < 47000)
        {
            hb = 32000;
            lb = (float)2.8;
            tb = (float)-85.5;
            return ((altitude.value - hb) * lb + tb) +273.15f;
        }
        else if (altitude.value < 51000)
        {
            hb = 47000;
            lb = 0;
            tb = (float)-43.5;
            return ((altitude.value - hb) * lb + tb) +273.15f;
        }
        else if (altitude.value < 71000)
        {
            hb = 51000;
            lb = (float)-2.8;
            tb = (float)-43.5;
            return ((altitude.value - hb) * lb + tb) +273.15f;
        }
        else if (altitude.value < 74999)
        {
            hb = 71000;
            lb = -2;
            tb = (float)12.5;
            return ((altitude.value - hb) * lb + tb) +273.15f;
        }
        {
            return 0;
        }
    }
    public float CalculateAirPressure()
    {
        float lb;
        float tb;
        int pab;
        float hb;
        
        if (altitude.value < 11000)
        {
            hb = 0;
            lb = -6.5f;
            tb = 15;
            pab = 101325;
        }
        else if (altitude.value < 20000)
        {
            hb = 11000;
            lb = 0;
            tb = (float)-97.5;
            pab = 22632;
        }
        else if (altitude.value<32000)
        {
            hb = 20000;
            lb = 1;
            tb = (float)-97.5;
            pab = 5475; 
        }
        else if (altitude.value < 47000)
        {
            hb = 32000;
            lb = (float)2.8;
            tb = (float)-85.5;
            pab = 868; 
        }
        else if (altitude.value < 51000)
        {
            hb = 47000;
            lb = 0;
            tb = (float)-43.5;
            pab = 111; 
        }
        else if (altitude.value < 71000)
        {
            hb = 51000;
            lb = (float)-2.8;
            tb = (float)-43.5;
            pab = 67; 
        }
        else if (altitude.value < 74999)
        {
            hb = 71000;
            lb = -2;
            tb = (float)12.5;
            pab = 4; 
        }
        else
        {
            return 0;
        }

        if (lb == 0)
        {
            return Mathf.Pow(pab,(float)((9.81 * 28*(altitude.value - hb))/(8.31*tb)));
        }
        return pab * Mathf.Pow(tb/(tb + lb*(altitude.value - hb)),(9.81f * 28) / (8.31f * lb));
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

    public Vector3 CreateVectorBaseOnLenghtAndLenght(float lenght, float anglebetweenvectorandy,
        float anglebetweenprojectvectorandx)
    {
        float lenghtprojectvector = Mathf.Sin(anglebetweenvectorandy);
        return new Vector3(lenghtprojectvector * Mathf.Cos(anglebetweenprojectvectorandx),
            Mathf.Cos(anglebetweenvectorandy) * lenght,
            lenghtprojectvector * Mathf.Cos(anglebetweenprojectvectorandx));
    }
}
