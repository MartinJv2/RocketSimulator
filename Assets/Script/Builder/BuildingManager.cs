using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject Parent;
    public GameObject object_loading;
    private Vector3 position;
    private RaycastHit hits_ground;
    public float gridSize;
    [SerializeField] private LayerMask LayerMask;
    public bool canPlace;
    public bool first;
    [SerializeField] private Material[] showingplaceable;
    
    void Update()
    {
        if (object_loading != null)
        {
            if (OverlapSphere(Gridsnap(position.x),
                    Gridsnap(position.y),
                    Gridsnap(position.z), 0.6f).Length >= 2 && OverlapSphere(Gridsnap(position.x),
                    Gridsnap(position.y),
                    Gridsnap(position.z), 0.0001f).Length == 2)
            {
                canPlace = true;
            }
            if (OverlapSphere(Gridsnap(position.x),
                    Gridsnap(position.y),
                    Gridsnap(position.z), 0.6f).Length !<= 2 && first == false || OverlapSphere(Gridsnap(position.x),
                    Gridsnap(position.y),
                    Gridsnap(position.z), 0.0001f).Length != 2 && first == false)
            {
                canPlace = false;
            }
            
            object_loading.transform.position = new Vector3(
                Gridsnap(position.x),
                Gridsnap(position.y),
                Gridsnap(position.z));

            UpdateColor();
            
            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                object_loading.GetComponent<MeshRenderer>().material = showingplaceable[2];
                object_loading = null;
                canPlace = false;
                if(first)
                {
                    first = false;
                } 
            
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

    void UpdateColor()
    {
        if (canPlace)
        {
            object_loading.GetComponent<MeshRenderer>().material = showingplaceable[0];
        }
        else
        {
            object_loading.GetComponent<MeshRenderer>().material = showingplaceable[1];
        }
    }

    Collider[] OverlapSphere(float posx, float posy, float posz, float size)
    {
        return Physics.OverlapSphere(new Vector3(posx, posy, posz), size);

    }
}