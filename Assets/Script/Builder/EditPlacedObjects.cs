using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EditPlacedObjects : MonoBehaviour
{
    public GameObject selectedObj;
    
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
        
        if(Input.GetMouseButtonDown(1))
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
}
