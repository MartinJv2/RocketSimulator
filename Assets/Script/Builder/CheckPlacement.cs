using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour

{
    private BuildingManager buildingmanager;
    //public bool canPlace;
    
    void Start()
    {
        buildingmanager = GameObject.Find("Buttons").GetComponent<BuildingManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            buildingmanager.canPlace = true;
            print(buildingmanager.canPlace);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            buildingmanager.canPlace = false;
            print(buildingmanager.canPlace);
        }
    }
}
