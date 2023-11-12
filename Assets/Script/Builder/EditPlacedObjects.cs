using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class EditPlacedObjects : MonoBehaviour
{
    
    public float gridsize;
    private GameObject floor;
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
    public bool ismouving;
    private Vector3 _position;
    private CapsuleCollider _box;
    [SerializeField] private LayerMask layermask;
    private bool _canplace;
    private Mesh _mesh;
    private Vector3 _scale;
    private Dictionary<Collider, Boolean> _canplaceobject = new Dictionary<Collider, Boolean>();
    private bool _iscollinding = false;
    

    private bool _CanPlace
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
    public bool IsSelected
    {
        get {return _isselected;}
        set
        {
            _isselected = value;
            ismouving = value;
            _outline.enabled = value;
            if (!_isselected)
            {
                SetMaterial(materialslist.finish);
            }
        }
    }

    private void Start()
    {
        _outline = gameObject.AddComponent<Outline>();
        _outline.OutlineColor = Color.black;
        IsSelected = true;
        _box = GetComponent<CapsuleCollider>();
        floor = GameObject.Find("Floor");
        _mesh = GetComponent<MeshFilter>().mesh;
        _scale = RoundVector3(multiplyVector3byVector3(gameObject.transform.localScale, _mesh.bounds.size));
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layermask))
        {
            _position = GridSnapCoordonate(hit.point);
            _position.y = floor.transform.position.y;
            if (IsSelected)
            {
                gameObject.transform.position = _position;
                if (_iscollinding)
                {
                    if (_canplaceobject.ContainsValue(false))
                    {
                        _CanPlace = false;
                    }
                    else if (_canplaceobject.ContainsValue(true))
                    {
                        _CanPlace = true;
                    }
                    Debug.Log("Number of collider" + _canplaceobject.Count);
                }
                else if (GameObject.FindGameObjectsWithTag("Object").Length == 1)
                {
                    _CanPlace = true;
                }
                else
                {
                    _CanPlace = false;
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && _CanPlace)
        {
            IsSelected = !IsSelected;
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
        if (other.CompareTag("Object") && ismouving)
        {
            _canplaceobject[other] = IsValidePosition(other);
            
            Debug.Log(gameObject.name + IsValidePosition(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Object") && ismouving)
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
        if (other.CompareTag("Object") && ismouving)
        {
            _iscollinding = true;
        }
    }

    private bool InRange(float center1, float center2, float offset1, float offset2)
    {
        
        Debug.Log("Offset: "+offset1);
        Debug.Log("Min center 1: "+ (center1 - offset1));
        Debug.Log("Max center 2: "+ (center2 + offset2));
        Debug.Log("Max center 1: "+(center1 + offset2));
        Debug.Log("Min center 2: "+ (center2 - offset1));
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
        Debug.Log("Size: "+_scale);
        Debug.Log("New collider");
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
