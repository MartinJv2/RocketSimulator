using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PhysiqueEngine : MonoBehaviour
{
    public double GravityAcelleration;
    public float MotorForce;
    public float MotorIgniteTime;
	public float Weight;
	
	private bool IsRunning = false;
	private float speed = 0;
	private float TimeUntilStart = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
	    if (IsRunning)
	    {
		    TimeUntilStart += Time.deltaTime;
		    speed += AddGravityBaseOnTime();
		    speed += AddMotorFocreBaseOnTime();
		    Debug.Log("Speed: " + speed.ToString());
		    transform.Translate(0, speed * Time.deltaTime, 0);
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

	public void StartLaunch()
	{
		IsRunning = true;
	}

	public void StopLaunch()
	{
		IsRunning = false;
	}
	
}