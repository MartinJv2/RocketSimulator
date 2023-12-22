using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewLayer : MonoBehaviour
{
    public GameObject floor;
    public float height;
    public float minHeight;
    public float maxHeight;
    
    void Update()
    {   
        floor.transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }
    
    public void SelectHeightChange(int heightincrease)
    {
        height += heightincrease;
        if (height <= minHeight)
        {
            height = minHeight;
        }
        if (height >= maxHeight)
        {
            height = maxHeight;
        }
    } 
}
