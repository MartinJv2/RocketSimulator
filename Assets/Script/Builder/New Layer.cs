using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewLayer : MonoBehaviour
{
    public GameObject floor;
    public float height;
    
    void Update()
    {   
        floor.transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }
    
    public void SelectHeightChange(int heightincrease)
    {
        height += heightincrease;
        if (height <= 0)
        {
            height = 0;
        }
        if (height >= 5)
        {
            height = 5;
        }
    } 
}
