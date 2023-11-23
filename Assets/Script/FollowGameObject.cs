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
        gameObject.transform.position = _followgameobject.transform.position;
    }
}
