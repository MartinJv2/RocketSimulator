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
    }

    public DisplayInfo displayinfo;


    private bool _isrunning;
    private float _speed;
    public float _altitude;
    private float _timeuntilstart;
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
            _timeuntilstart += Time.deltaTime;
            if (!isinspace)
            {
                
                _speed += AddGravityBaseOnTime();
            }
            _speed += AddMotorFocreBaseOnTime();
            _altitude += _speed * Time.deltaTime;
            if (!isinspace)
            {
                _objectlocation.transform.position = new Vector3(_objectlocation.transform.position.x, _altitude/20,
                    _objectlocation.transform.position.z);
            }
            displayinfo.altitude.element.text = displayinfo.altitude.value + Mathf.Round(_altitude) + "m";
            displayinfo.speed.element.text = displayinfo.speed.value + Mathf.Round(_speed) + " m/s";
            if (beginspace > _altitude && _altitude > beginhauteatmosphere && currentscence != hauteatmospherescene)
            {
                SceneManager.LoadScene(hauteatmospherescene, LoadSceneMode.Single);
                currentscence = hauteatmospherescene;
            }
            else if (_altitude > beginspace && currentscence != spacescene)
            {
                SceneManager.LoadScene(spacescene, LoadSceneMode.Single);
                currentscence = spacescene;
                isinspace = true;
            }
        }
    }

    private float AddGravityBaseOnTime()
    {
        return (float)(rocketparameter.gravityacelleration * Time.deltaTime);
    }

    private float AddMotorFocreBaseOnTime()
    {
        float force = 0;
        foreach (MotorProperty motor in rocketparameter.motorlist)
        {
            if (_timeuntilstart <= motor.ignitetime)
            {
                force += (CalculateAccellerationBaseOnForceAndWeight(motor.force) * Time.deltaTime);
            }
        }

        return force;
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
}