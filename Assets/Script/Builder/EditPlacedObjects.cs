using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class EditPlacedObjects : MonoBehaviour
{
    public GameObject selectedObj;
    public TextMeshProUGUI objNameText;
    private BuildingManager buildingManager;

    private void Start()
    {
        buildingManager = GameObject.Find("Buttons").GetComponent<BuildingManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Object"))
                {
                    Select(hit.collider.gameObject);
                }
            }

        }
        
        if(Input.GetMouseButtonDown(1) && selectedObj != null)
        {
            Deselect();
        }
        
        void Select(GameObject obj)
        {
            if(obj == selectedObj) return;
            if(selectedObj != null) Deselect();
            Outline outline = obj.GetComponent<Outline>();
            if(outline == null) obj.AddComponent<Outline>();
            else outline.enabled = true;
            selectedObj = obj;
        }
        
        void Deselect()
        {
            selectedObj.GetComponent<Outline>().enabled = false;
            selectedObj = null;
        }
        
    }

    /*void Move()
    {
        buildingManager.object_loading = selectedObj;
    }

    void Delete()
    {
        GameObject objToDestroy = selectedObj;
        Deselect();
    }*/
    
}
