using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraMouvement : MonoBehaviour
{ 
private float y_rotation;
private float size;
public float speed;
public float scale;

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
    if (Input.GetKey(KeyCode.S))
    {
        size += scale;
    }
    if (Input.GetKey(KeyCode.W))
    {
        size -= scale;
    }
    if (size < 0.5)
    {
        size = 0.5f;
    }
    gameObject.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x,y_rotation,transform.rotation.z));
    gameObject.transform.localScale = new Vector3(size, size, size);

}

}