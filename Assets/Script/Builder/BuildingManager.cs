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
    public float gridSize;
    [SerializeField] private LayerMask LayerMask;
    public bool canPlace;
    
    
    void Update()
    {
        if (object_loading != null)
        {
            object_loading.transform.position = new Vector3(
                Gridsnap(position.x),
                Gridsnap(position.y),
                Gridsnap(position.z));
            

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                    object_loading = null;
                    canPlace = false;
            }
        }
        
    }
    

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hits_ground, 1000, LayerMask))
        {
           position = hits_ground.point;
        }   
    }

    float Gridsnap(float pos)
    {
        float rounding = pos % gridSize;
        pos -= rounding;
        if (rounding > (gridSize / 2))
        {
            pos += gridSize;
        }

        return pos;
    }
    
    public void SelectObject(int index)
    {
        if (object_loading != null)
        {
            Destroy(object_loading);
        }
        object_loading = Instantiate(objects[index], position, transform.rotation, Parent.transform);   
        
    }
    
}