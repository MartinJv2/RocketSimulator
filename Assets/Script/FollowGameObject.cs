using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    public string followgameobjectname;
    private GameObject _followgameobject;
    void Start()
    {
        _followgameobject = GameObject.Find(followgameobjectname);
    }
    void Update()
    {
        if (GameObject.Find("physicManager").GetComponent<PhysiqueEngine>()._altitude > 1000)
        {
            gameObject.transform.rotation = Quaternion.Euler(70.0f, 0, 0);
            gameObject.transform.position = new Vector3(-1, _followgameobject.transform.position.y+10f, -5);
        }
        
    }
}