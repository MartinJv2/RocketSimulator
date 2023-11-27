using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMouvement : MonoBehaviour
{ 
private float y_rotation;
private float x_rotation;
public float size;
public float speed;
public float scale;
public int minx;
public int maxx;

void Update()
{
    if (Input.GetKey(KeyCode.A))
    {
        y_rotation += speed;
    }
    if (Input.GetKey(KeyCode.D))
    {
        y_rotation -= speed;
    }
    if (Input.GetKey(KeyCode.S) && x_rotation > minx)
    {
        x_rotation -= speed;
    }
    if (Input.GetKey(KeyCode.W) && x_rotation < maxx)
    {
        x_rotation += speed;
    }

    size += -Input.mouseScrollDelta.y * scale;
    
    if (size < 0.25 || size > 5)
    {
        size = 0.25f;
    }
    gameObject.transform.rotation = Quaternion.Euler(new Vector3(x_rotation,y_rotation,transform.rotation.z));
    gameObject.transform.localScale = new Vector3(size, size, size);

    
    
}

}