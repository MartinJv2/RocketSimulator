using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMouvement : MonoBehaviour
{
    public CameraTransform cameratransform;
    public float y_rotation;
    public float x_rotation;
    private float size;
    public float speed;
    public float scale;
    public float minx;
    public float maxx;
    public float close;
    public float far;
    public float startingSize;
    public Quaternion startingRo;

    void Start()
    {
        cameratransform.camera = this.gameObject;
        gameObject.transform.rotation = Quaternion.Euler(cameratransform.rotation);
        gameObject.transform.localScale = cameratransform.scale;
        size = startingSize;
        startingRo = Quaternion.Euler(new Vector3(x_rotation, y_rotation,gameObject.transform.rotation.z));
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            y_rotation += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            y_rotation -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            x_rotation -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            x_rotation += speed * Time.deltaTime;
        }

        if (x_rotation > maxx)
        {
            x_rotation = maxx;
        }
        else if (x_rotation < minx)
        {
            x_rotation = minx;
        }
        if (y_rotation > 360)
        {
            y_rotation -= 360;
        }
        else if (y_rotation < 0)
        {
            y_rotation += 360;
        }
        size += -Input.mouseScrollDelta.y * scale;

        if (size < close || size > far)
        {
            size = close;
        }
        cameratransform.rotation = new Vector3(x_rotation, y_rotation, transform.rotation.z);
        gameObject.transform.rotation = Quaternion.Euler(cameratransform.rotation);
        cameratransform.scale = new Vector3(size, size, size);
        gameObject.transform.localScale = cameratransform.scale;
    }
    public void reset()
    {
        gameObject.transform.localScale = new Vector3(startingSize, startingSize, startingSize);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(startingRo.x, startingRo.y, startingRo.z));
        size = startingSize;
        x_rotation = startingRo.x;
        y_rotation = startingRo.y;
    }
}