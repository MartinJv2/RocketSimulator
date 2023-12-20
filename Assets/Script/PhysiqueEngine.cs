using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static System.Linq.Enumerable;
using UnityEngine.SceneManagement;

public class PhysiqueEngine : MonoBehaviour
{
    public string objectlocation;
    public string hauteatmospherescene;
    public int beginhauteatmosphere;
    public string spacescene;
    public int beginspace;
    private string currentscence = "Onearh";
    private GameObject _objectlocation;
    private bool isinspace;
    [Serializable]
    public struct RocketParameter
    {
        public double gravityacelleration;
        public float acceleratorspeed;
        
        [HideInInspector]
        public float weight;
        [HideInInspector]
        public List<MotorProperty> motorlist;
    }

    public RocketParameter rocketparameter;

    [Serializable]
    public struct ToggleButton
    {
        public TextMeshProUGUI button;
        public string stoptext;
        public string runtext;
    }

    public ToggleButton togglebutton;

    [Serializable]
    public struct TextDependOnenValue
    {
        public TextMeshProUGUI element;
        public string value;
    }

    [Serializable]
    public struct DisplayInfo
    {
        public TextDependOnenValue altitude;
        public TextDependOnenValue speed;
        public TextDependOnenValue timeuntilstart;
    }

    public DisplayInfo displayinfo;


    private bool _isrunning;
    private float _meanspeed;
    public double _altitude;
    private float _timesincestart;
    private MeshRenderer _renderer;
    private Transform _currentchild;
    private float aceleration = 0;
    private double _speed = 0;

    private void Start()
    {
        _objectlocation = GameObject.Find(objectlocation);
        rocketparameter.weight = CalculateTotalWightFromParent();
        rocketparameter.motorlist = FindAllMotorFromParent();
        togglebutton.button.text = togglebutton.runtext;
        _renderer = GetComponent<MeshRenderer>();
    }

    private float CalculateTotalWightFromParent()
    {
        
        float weight = 0;
        foreach (int index in Range(0,_objectlocation.transform.childCount))
        {
            _currentchild = _objectlocation.transform.GetChild(index);
            weight += _currentchild.transform.GetComponent<BaseProperty>().weight;
        }
        return weight;
    }

    private List<MotorProperty> FindAllMotorFromParent()
    {
        List<MotorProperty> motorlist = new List<MotorProperty>();
        foreach (int index in Range(0, _objectlocation.transform.childCount))
        {
            _currentchild = _objectlocation.transform.GetChild(index);
            if (_currentchild.transform.GetComponent<MotorProperty>() != null)
            {
                motorlist.Add(_currentchild.gameObject.GetComponent<MotorProperty>());
            }
        }

        return motorlist;
    }
    private void FixedUpdate()
    {
        if (_isrunning)
        {
            double force = 0;
            double acceleration;
            double initialespeed = _speed;
            double finalspeed = 0;
            _timesincestart += Time.deltaTime;
            displayinfo.timeuntilstart.element.text = displayinfo.timeuntilstart.value + _timesincestart;
            if (!isinspace)
            {
                force += CalculateGravityFroce();
            }
            force += AddMotorForcenBaseOnTime();
            acceleration = CalculateAccellerationBaseOnForceAndWeight(force);
            finalspeed = (acceleration * Time.deltaTime) + initialespeed;
            _altitude = (initialespeed + finalspeed) * _timesincestart/2;
            _speed = finalspeed;
            _objectlocation.transform.position = new Vector3(_objectlocation.transform.position.x, (float)(_altitude/60), 
                _objectlocation.transform.position.z);
            displayinfo.altitude.element.text = displayinfo.altitude.value + (_altitude) + "m";
            displayinfo.speed.element.text = displayinfo.speed.value + (_speed*2) + " m/s";
            if (_altitude > beginspace && currentscence != spacescene)
            {
                SceneManager.LoadScene(spacescene, LoadSceneMode.Single);
                currentscence = spacescene;
                isinspace = true;
            }
            else if (_altitude > beginhauteatmosphere && currentscence != spacescene && currentscence != hauteatmospherescene)
            {
                SceneManager.LoadScene(hauteatmospherescene, LoadSceneMode.Single);
                currentscence = hauteatmospherescene;
                isinspace = false;
            }
        }
    }

    private float CalculateGravityFroce()
    {
        return (float)(rocketparameter.gravityacelleration/rocketparameter.weight);
    }

    private float AddMotorForcenBaseOnTime()
    {
        float force = 0;
        foreach (MotorProperty motor in rocketparameter.motorlist)
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
                    force += (motor.force * motor.duration) - motor.generatedtrusted;
                    motor.generatedtrusted += force;
                }
                motor.GetComponent<ParticleSystem>().Stop(false,ParticleSystemStopBehavior.StopEmitting);
            }
        }
        return force;
    }

    private double CalculateAccellerationBaseOnForceAndWeight(double force)
    {
        return (force / rocketparameter.weight);
    }

    private float DragForce()
    {
        return 0;
    }

    public void ToggleLaunch()
    {
        if (_isrunning)
        {
            _isrunning = false;
            togglebutton.button.text = togglebutton.runtext;
            
        }
        else
        {
            _isrunning = true;
            togglebutton.button.text = togglebutton.stoptext;
        }
    }
}