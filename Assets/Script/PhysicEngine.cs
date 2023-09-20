using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicEngine : MonoBehaviour
{
    public float GravityAcelleration;
    
	private float speed = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {	
		speed += AddGravityBaseOnTime();
        transform.Translate(0, speed * Time.deltaTime, 0);
	}

    private float AddGravityBaseOnTime()
    {
	    return GravityAcelleration * Time.deltaTime;
    }
    
}
