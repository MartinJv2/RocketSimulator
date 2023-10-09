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
	private float TimeUntilStart = 0;

	void Start()
	{
		ToggleLaunchButtonText.text = RunText;
	}
    void FixedUpdate()
    {
	    if (IsRunning)
	    {
		    TimeUntilStart += Time.deltaTime;
		    speed += AddGravityBaseOnTime();
		    speed += AddMotorFocreBaseOnTime();
		    transform.Translate(0, speed * Time.deltaTime, 0);
		    AltitudeText.text = AltitudeContentText + Mathf.Round(transform.position.y).ToString() + "m";
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