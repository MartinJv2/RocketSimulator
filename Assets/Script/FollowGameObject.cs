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
        if (GameObject.Find("physicManager").GetComponent<PhysiqueEngine>()._altitude > 3000)
        {
            if (GameObject.Find("physicManager").GetComponent<PhysiqueEngine>()._altitude < 100000)
            {
                gameObject.transform.position = new Vector3(_followgameobject.transform.position.x, _followgameobject.transform.position.y-1.5f, _followgameobject.transform.position.z-4);   
            }
            else
            {
                gameObject.transform.position = new Vector3(_followgameobject.transform.position.x, _followgameobject.transform.position.y, _followgameobject.transform.position.z);   
            }
                
        }
        
    }
}