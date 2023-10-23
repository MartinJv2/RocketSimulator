using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
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
    [SerializeField] private Material[] showingplaceable;
    private bool placed = false;
    
    void Update()
    {
        if (object_loading != null)
        {
            object_loading.transform.position = new Vector3(
                Gridsnap(position.x),
                Gridsnap(position.y),
                Gridsnap(position.z));
            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(Gridsnap(position.x), Gridsnap(position.y), Gridsnap(position.z)), 0.6f);
            Collider[] minihitcollider = Physics.OverlapSphere(new Vector3(Gridsnap(position.x), Gridsnap(position.y + 0.1f), Gridsnap(position.z)), 0.0001f);
            if (hitColliders.Length <= 2 && placed)
            {
                canPlace = false;
            }
            else
            {
                canPlace = true;
            }
            UpdateColor();
            

            if (Input.GetMouseButtonDown(0) && canPlace && hitColliders.Length >= 2 && minihitcollider.Length==2)
            {
                object_loading.GetComponent<MeshRenderer>().material = showingplaceable[2];
                object_loading = null;
                canPlace = false;
                if(placed==false)
                {
                     placed = true;
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
}