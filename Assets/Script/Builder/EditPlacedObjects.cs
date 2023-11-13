using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class EditPlacedObjects : MonoBehaviour
{
    
    public float gridsize;
    private GameObject _floor;
    private GameObject _menu;
    [Serializable]
    public struct MaterialsList
    {
        public Material wrongplacement;
        public Material correctplacement;
        public Material finish;
    }
    public MaterialsList materialslist;
    private Outline _outline;
    private bool _isselected; 
    [HideInInspector]
    private bool _ismouving;

    public bool ismouving
    {
        get { return _ismouving; }
        set
        {
            _ismouving = value;
            if (!ismouving)
            {
                SetMaterial(materialslist.finish);
            }
        }
    }
    private Vector3 _position;
    private CapsuleCollider _box;
    [SerializeField] private LayerMask layermask;
    private bool _canplace;
    private Mesh _mesh;
    private Vector3 _scale;
    private Dictionary<Collider, Boolean> _canplaceobject = new Dictionary<Collider, Boolean>();
    private bool _iscollinding = false;
    

    public bool CanPlace
    {
        get { return _canplace;}
        set
        {
            _canplace = value;
            if (_canplace)
            {
                SetMaterial(materialslist.correctplacement);
            }
            else
            {
                SetMaterial(materialslist.wrongplacement);
            }
        }
    }

    

    private void Start()
    {
        _menu = GameObject.Find("Menu");
        _ismouving = true;
        _box = GetComponent<CapsuleCollider>();
        _floor = GameObject.Find("Floor");
        _mesh = GetComponent<MeshFilter>().mesh;
        _scale = RoundVector3(multiplyVector3byVector3(gameObject.transform.localScale, _mesh.bounds.size));
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (_ismouving)
        {
            Physics.Raycast(ray, out hit, 1000);
            _position = GridSnapCoordonate(hit.point);
            _position.y = _floor.transform.position.y;
            gameObject.transform.position = _position;
            if (_iscollinding)
            {
                if (_canplaceobject.ContainsValue(false))
                {
                    CanPlace = false;
                }
                else if (_canplaceobject.ContainsValue(true))
                {
                    CanPlace = true;
                }
            }
            else if (GameObject.FindGameObjectsWithTag("Object").Length == 1)
            {
                CanPlace = true;
            }
            else
            {
                CanPlace = false;
            }
        }
    }

    private void OnMouseOver()
    {
        if (_canplace && Input.GetMouseButtonDown(0) && ismouving)
        {
            ismouving = false;
            _menu.GetComponent<MenuManager>().UnSelectObject();
        }
        else if (Input.GetMouseButtonDown(0) && !ismouving)
        {
            _menu.GetComponent<MenuManager>().Selectedobject = gameObject;
        }
    }

    private Vector3 GridSnapCoordonate(Vector3 pos)
    {
        return new Vector3(GridsnapValue(pos.x), GridsnapValue(pos.y), GridsnapValue(pos.z));
    }

    private int GridsnapValue(float pos)
    {
        return (int)Math.Round(pos);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Object") && _ismouving)
        {
            _canplaceobject[other] = IsValidePosition(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Object") && _ismouving)
        {
            _canplaceobject.Remove(other);
            if (_canplaceobject.Count == 0)
            {
                _iscollinding = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object") && _ismouving)
        {
            _iscollinding = true;
        }
    }

    private bool InRange(float center1, float center2, float offset1, float offset2)
    {
        return center1 + offset1 > center2 - offset2 && center1 - offset1 < center2 + offset2;
    }

    private bool IsInside(Vector3 position1, Vector3 position2, Vector3 offset1, Vector3 offset2)
    {
        return InRange(position1.x, position2.x, offset1.x,offset2.x)&&
               InRange(position1.y, position2.y, offset1.y,offset2.y)&&
               InRange(position1.z, position2.z, offset1.z, offset2.z);
    }

    private Vector3 multiplyVector3byVector3(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    private void SetMaterial(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
    }

    private bool IsValidePosition(Collider collider)
    {
        Mesh mesh = collider.gameObject.GetComponent<MeshFilter>().mesh;
        Vector3 colliderscale =
            RoundVector3(multiplyVector3byVector3(gameObject.transform.localScale, _mesh.bounds.size));
        if (IsInside(gameObject.transform.position, collider.transform.position, _scale / 2, colliderscale/2))
        {
            return false;
        }
        return true;
    }

    private Vector3 RoundVector3(Vector3 vector)
    {
        return new Vector3((float)Math.Round(vector.x), (float)Math.Round(vector.y), (float)Math.Round(vector.z));
    }
}
