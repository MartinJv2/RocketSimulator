using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraMouvement : MonoBehaviour
{ 
private float y_rotation;
private float x_rotation;
private float size;
public float speed;
public float scale;

private void Start()
{
    size = 0.5f;
}

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
        x_rotation -= speed;
    }
    if (Input.GetKey(KeyCode.W))
    {
        x_rotation += speed;
    }
    if (size < 0)
    {
        size = 0;
    }
    gameObject.transform.rotation = Quaternion.Euler(new Vector3(x_rotation,y_rotation,transform.rotation.z));
    gameObject.transform.localScale = new Vector3(size, size, size);

}

}