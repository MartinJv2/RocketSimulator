using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CameraMouvement : MonoBehaviour
{ 
private float y_rotation;
private float x_rotation;
public float size;
public float speed;
public float scale;
public float minx;
public float maxx;
public float startingSize;
public Quaternion startingRo;

void Start()
{
    startingSize = size;
    startingRo = Quaternion.Euler(new Vector3(x_rotation, y_rotation,gameObject.transform.rotation.z));
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
    if (Input.GetKey(KeyCode.S) && x_rotation > minx)
    {
        x_rotation -= speed;
    }
    if (Input.GetKey(KeyCode.W) && x_rotation < maxx)
    {
        x_rotation += speed;
    }

    size += -Input.mouseScrollDelta.y * scale;
    
    gameObject.transform.rotation = Quaternion.Euler(new Vector3(x_rotation,y_rotation,transform.rotation.z));
    gameObject.transform.localScale = new Vector3(size, size, size);

    /*if (Scene.buildIndex = 1)
    {
       startingSize = 1;
       startingRo = Quaternion.Euler(new Vector3(-40, 0,0));
    }*/
}

public void reset()
{
    gameObject.transform.localScale = new Vector3(startingSize, startingSize, startingSize);
    gameObject.transform.rotation = Quaternion.Euler(new Vector3(startingRo.x, startingRo.y, startingRo.z));
    size = startingSize;
    x_rotation = startingRo.x;
    y_rotation = startingRo.y;
    Debug.Log("reset camera");
}

}