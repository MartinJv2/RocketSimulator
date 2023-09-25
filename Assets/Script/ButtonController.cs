using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject Parent;
    private GameObject object_loading;
    private Vector3 position;
    private RaycastHit hits_ground;
    [SerializeField] private LayerMask layerMask;

    void Update()
    {
        if (object_loading != null)
        {   
            object_loading.transform.position = position;
            

            if (Input.GetMouseButtonDown(0))
            {
                object_loading = null;
            }
        }
        
    }
    

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hits_ground, 1000, layerMask))
        {
           position = hits_ground.point;
        }   
    }

    public void SelectObject(int index)
    {
        object_loading = Instantiate(objects[index], position, transform.rotation, Parent.transform);
        //object_loading.SetActive(false);
    }

}