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
    public string basseatmospherescene;
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
    public float _altitude;
    private float _timesincestart;
    private MeshRenderer _renderer;
    private Transform _currentchild;

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
            _timesincestart += Time.deltaTime;
            displayinfo.timeuntilstart.element.text = displayinfo.timeuntilstart.value + _timesincestart;
            if (!isinspace)
            {
                
                _meanspeed += AddGravitySpeedBaseOnTime();
            }

            _meanspeed += AddMotorAccelerationBaseOnTime() * Time.deltaTime;
            if (_altitude != 0 || _meanspeed > 0)
            {
                 _altitude = _meanspeed * _timesincestart;
                 _objectlocation.transform.position = new Vector3(_objectlocation.transform.position.x, _altitude/120, 
                     _objectlocation.transform.position.z);
            }

            if (_altitude != 0)
            {
                displayinfo.speed.element.text = displayinfo.speed.value + (_meanspeed*2) + " m/s";   
            }
            displayinfo.altitude.element.text = displayinfo.altitude.value + (_altitude) + "m";
            if (_altitude > beginspace && currentscence != spacescene)
            { SceneManager.LoadScene(spacescene, LoadSceneMode.Single); currentscence = spacescene;
                isinspace = true;
            }
            if (_altitude < beginhauteatmosphere && currentscence == hauteatmospherescene)
            { SceneManager.LoadScene(basseatmospherescene, LoadSceneMode.Single);
                currentscence = basseatmospherescene;
            }
            else if (_altitude > beginhauteatmosphere && currentscence != spacescene && currentscence != hauteatmospherescene)
            {
                SceneManager.LoadScene(hauteatmospherescene, LoadSceneMode.Single);
                currentscence = hauteatmospherescene;
                isinspace = false;
            }
        }
    }

    private float AddGravitySpeedBaseOnTime()
    {
        return (float)(rocketparameter.gravityacelleration * Time.deltaTime);
    }

    private float AddMotorAccelerationBaseOnTime()
    {
        float acceleration = 0;
        foreach (MotorProperty motor in rocketparameter.motorlist)
        {
            if (_timesincestart <= motor.outOfFuel)
            {
                motor.GetComponent<ParticleSystem>().Play();
                acceleration += (CalculateAccellerationBaseOnForceAndWeight(motor.force * _timesincestart));
            }
            else
            {
                motor.GetComponent<ParticleSystem>().Stop(false,ParticleSystemStopBehavior.StopEmitting);
            }
        }
        return acceleration;
    }

    private float CalculateAccellerationBaseOnForceAndWeight(float force)
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

    private void Update()
    {
        if (_altitude < 0 && _meanspeed < 0)
        {
            _altitude = 0;
            displayinfo.speed.element.text = displayinfo.speed.value + 0 + " m/s";
        }
    }
}