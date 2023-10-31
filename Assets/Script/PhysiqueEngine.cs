using System;
using UnityEngine;
using TMPro;

public class PhysiqueEngine : MonoBehaviour
{
    [Serializable]
    public struct RocketParameter
    {
        public double gravityacelleration;
        public float motorforce;
        public float motorignitetime;
        public float weight;
        public float acceleratorspeed;
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


    private bool _isrunning = false;
    private float _speed = 0;
    private float _altitude;
    private float _timeuntilstart = 0;
    private MeshRenderer _renderer;

    private void Start()
    {
        togglebutton.button.text = togglebutton.runtext;
        _renderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        if (_isrunning)
        {
            _timeuntilstart += Time.deltaTime;
            _speed += AddGravityBaseOnTime();
            _speed += AddMotorFocreBaseOnTime();
            _altitude += _speed * Time.deltaTime;
            if (_speed >= 0)
            {
                _renderer.material.mainTextureOffset = new Vector2(0, (float)((Math.Sqrt(_speed) +_altitude) * rocketparameter.acceleratorspeed));
            }
            else
            {
                _renderer.material.mainTextureOffset = new Vector2(0, (float)((Math.Sqrt(-_speed) +_altitude) * rocketparameter.acceleratorspeed));

            }
            displayinfo.altitude.element.text = displayinfo.altitude.value + Mathf.Round(_altitude) + "m";
            displayinfo.speed.element.text = displayinfo.speed.value + Mathf.Round(_speed) + " m/s";
        }
    }

    private float AddGravityBaseOnTime()
    {
        return (float)(rocketparameter.gravityacelleration * Time.deltaTime);
    }

    private float AddMotorFocreBaseOnTime()
    {
        if (_timeuntilstart >= rocketparameter.motorignitetime)
            return 0;
        else
            return (CalculateAccellerationBaseOnForceAndWeight(rocketparameter.motorforce) * Time.deltaTime);
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