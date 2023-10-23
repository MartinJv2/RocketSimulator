using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;

public class PhysiqueEngine : MonoBehaviour
{
    [Header("RocketInfo")]
	public double GravityAcelleration;
    public float MotorForce;
    public float MotorIgniteTime;
	public float Weight;
	public float accelerator = 1;
	[Header("LaunchButton")]
	public TMPro.TextMeshProUGUI ToggleLaunchButtonText;
	public string StopText = "Stop";
	public string RunText = "Run";
	[Header("FlightInfo")]
	public string AltitudeContentText = "Altitude: ";
	public TMPro.TextMeshProUGUI AltitudeText;
	public string SpeedContentText = "Speed: ";
	public TMPro.TextMeshProUGUI SpeedText;
	
	private bool IsRunning = false;
	private float speed = 0;
	private float altitude;
	private float TimeUntilStart = 0;
	private MeshRenderer _renderer;

	void Start()
	{
		ToggleLaunchButtonText.text = RunText;
		_renderer = GetComponent<MeshRenderer>();
	}
    void FixedUpdate()
    {
	    if (IsRunning)
	    {
		    TimeUntilStart += Time.deltaTime;
		    speed += AddGravityBaseOnTime();
		    speed += AddMotorFocreBaseOnTime();
		    altitude += speed * Time.deltaTime;
		    if (altitude < 0)
		    {
			    _renderer.material.mainTextureOffset = new Vector2(0, (float)-Math.Sqrt(Math.Abs(altitude))); 
		    }
		    else
		    {
			    _renderer.material.mainTextureOffset = new Vector2(0, (float)Math.Sqrt(altitude));
		    }
			    
		    
		    AltitudeText.text = AltitudeContentText + Mathf.Round(altitude).ToString() + "m";
		    SpeedText.text = SpeedContentText + Mathf.Round(speed).ToString() + " m/s";
	    }
    }

    private float AddGravityBaseOnTime()
    {
	    return (float)(GravityAcelleration * Time.deltaTime);
    }
	private float AddMotorFocreBaseOnTime()
	{
		if (TimeUntilStart >= MotorIgniteTime)
		{
			return 0;
		}
		else
		{
			return (float)(CalculateAccellerationBaseOnForceAndWeight(MotorForce) * Time.deltaTime);
		}
	}
	private float CalculateAccellerationBaseOnForceAndWeight(float force)
	{
		return (float)(force/Weight);
	}

	private float DragForce()
	{
		return 0;
	}

	public void ToggleLaunch()
	{
		if (IsRunning)
		{
			IsRunning = false;
			ToggleLaunchButtonText.text = RunText;
		}
		else
		{
			IsRunning = true;
			ToggleLaunchButtonText.text = StopText;
		}
	}
}