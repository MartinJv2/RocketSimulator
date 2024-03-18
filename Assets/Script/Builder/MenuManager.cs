using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private GameObject _selectedobject;
    private MoveObjects _editplacedobjects;
    public GameObject launchbutton;
    public LayerMask layermask;
    public TMP_InputField setweightinputfield;
    public TMP_InputField settrustinputfield;
    public TMP_InputField setdurationtinputfield;
    public GameObject motorparmeditor;
    public PhysicEngine physicengine;
    [SerializeField] public Slider slider_x;
    [SerializeField] public Slider slider_z;
    [SerializeField] public Slider slider_y;

    private void Start()
    {
        slider_x.onValueChanged.AddListener((v) =>
        {
            if (CheckConnections())
            {
                Selectedobject.transform.localScale = new Vector3(Selectedobject.transform.localScale.x/_selectedobject.GetComponent<BaseProperty>().last_x,Selectedobject.transform.localScale.y,Selectedobject.transform.localScale.z);
                Selectedobject.transform.localScale = new Vector3(v*Selectedobject.transform.localScale.x,Selectedobject.transform.localScale.y,Selectedobject.transform.localScale.z);
                if (v != _selectedobject.GetComponent<BaseProperty>().last_x)
                {
                    Selectedobject.GetComponent<MoveObjects>().ismouving = true;
                }
                _selectedobject.GetComponent<BaseProperty>().last_x = v;
                if(_selectedobject.GetComponent<BaseProperty>().last_x % 2 == 0)
                {
                    _selectedobject.GetComponent<BaseProperty>().decalement_x = 0.5f;
                }
                else
                {
                    _selectedobject.GetComponent<BaseProperty>().decalement_x = 0;
                }
                Selectedobject.GetComponent<MoveObjects>().UpdateScale();
            }
        });
        slider_z.onValueChanged.AddListener((v) =>
        {
            if (CheckConnections())
            {
                if(_selectedobject.name != ("Cone(Clone)"))
                {
                    Selectedobject.transform.localScale = new Vector3(Selectedobject.transform.localScale.x,Selectedobject.transform.localScale.y,Selectedobject.transform.localScale.z/_selectedobject.GetComponent<BaseProperty>().last_z);
                    Selectedobject.transform.localScale = new Vector3( Selectedobject.transform.localScale.x ,Selectedobject.transform.localScale.y,v*Selectedobject.transform.localScale.z);
                    if (v != _selectedobject.GetComponent<BaseProperty>().last_z)
                    {
                        Selectedobject.GetComponent<MoveObjects>().ismouving = true;
                    }
                    _selectedobject.GetComponent<BaseProperty>().last_z = v;
                }
                else
                {
                    Selectedobject.transform.localScale = new Vector3(Selectedobject.transform.localScale.x,Selectedobject.transform.localScale.y/_selectedobject.GetComponent<BaseProperty>().last_z,Selectedobject.transform.localScale.z);
                    Selectedobject.transform.localScale = new Vector3( Selectedobject.transform.localScale.x ,Selectedobject.transform.localScale.y*v,Selectedobject.transform.localScale.z);
                    if (v != _selectedobject.GetComponent<BaseProperty>().last_z)
                    {
                        Selectedobject.GetComponent<MoveObjects>().ismouving = true;
                    }
                    _selectedobject.GetComponent<BaseProperty>().last_z = v;
                }
                if(_selectedobject.GetComponent<BaseProperty>().last_z % 2 == 0)
                {
                    _selectedobject.GetComponent<BaseProperty>().decalement_z = 0.5f;
                }
                else
                {
                    _selectedobject.GetComponent<BaseProperty>().decalement_z = 0;
                }
                Selectedobject.GetComponent<MoveObjects>().UpdateScale();
            }
        });
        slider_y.onValueChanged.AddListener((v) =>
        {
            if (CheckConnections())
            {
                if(_selectedobject.name != ("Cone(Clone)"))
                {
                    Selectedobject.transform.localScale = new Vector3(Selectedobject.transform.localScale.x,Selectedobject.transform.localScale.y/_selectedobject.GetComponent<BaseProperty>().last_y,Selectedobject.transform.localScale.z);
                    Selectedobject.transform.localScale = new Vector3( Selectedobject.transform.localScale.x ,Selectedobject.transform.localScale.y*v,Selectedobject.transform.localScale.z);
                    if (v != _selectedobject.GetComponent<BaseProperty>().last_y)
                    {
                        Selectedobject.GetComponent<MoveObjects>().ismouving = true;
                    }
                    _selectedobject.GetComponent<BaseProperty>().last_y = v;
                }
                else
                {
                    Selectedobject.transform.localScale = new Vector3(Selectedobject.transform.localScale.x,Selectedobject.transform.localScale.y,Selectedobject.transform.localScale.z/_selectedobject.GetComponent<BaseProperty>().last_y);
                    Selectedobject.transform.localScale = new Vector3( Selectedobject.transform.localScale.x ,Selectedobject.transform.localScale.y,Selectedobject.transform.localScale.z*v);
                    if (v != _selectedobject.GetComponent<BaseProperty>().last_y)
                    {
                        Selectedobject.GetComponent<MoveObjects>().ismouving = true;
                    }
                    _selectedobject.GetComponent<BaseProperty>().last_y = v;
                }
                if(_selectedobject.GetComponent<BaseProperty>().last_y % 2 == 0)
                {
                    _selectedobject.GetComponent<BaseProperty>().decalement_y = 0.5f;
                }
                else
                {
                    _selectedobject.GetComponent<BaseProperty>().decalement_y = 0;
                }
                Selectedobject.GetComponent<MoveObjects>().UpdateScale();
            }
        });
    }

    [HideInInspector]
    public GameObject Selectedobject
    {
        get { return _selectedobject; }
        set
        {
            if (_selectedobject == value)
            {
                UnSelectObject();
            }
            else if (value == null)
            {
                if (_selectedobject != null)
                {
                    UnSelectObject();
                }

                _selectedobject = null;
            }
            else
            {
                if (_selectedobject != null)
                {

                    if (_selectedobject.GetComponent<MoveObjects>() != null)
                    {
                        if (_selectedobject.GetComponent<MoveObjects>().ismouving)
                        {
                            return;
                        }
                    }

                    UnSelectObject();
                }

                SelectObject(value);
            }
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layermask) && Input.GetMouseButtonDown(0))
        {
            if (hit.collider.gameObject.CompareTag("Object"))
            {
                Selectedobject = hit.collider.gameObject;
            }
        }
    }
    public void UnSelectObject()
    {
        _editplacedobjects = _selectedobject.GetComponent<MoveObjects>();
        if (_editplacedobjects.CanPlace)
        {
            _editplacedobjects.ismouving = false;
            _selectedobject.GetComponent<Outline>().enabled = false;
            _selectedobject = null;
        }

        if (!_editplacedobjects == null)
        {
            Hide();
            ShowLaunchButton();
        }
    }

    private void SelectObject(GameObject value)
    {
        if (_selectedobject != null)
        {
            UnSelectObject();
        }
        _selectedobject = value;
        slider_z.value = _selectedobject.GetComponent<BaseProperty>().last_z;
        slider_x.value = _selectedobject.GetComponent<BaseProperty>().last_x;
        slider_y.value = _selectedobject.GetComponent<BaseProperty>().last_y;
        _editplacedobjects = _selectedobject.GetComponent<MoveObjects>();
        setweightinputfield.text = _editplacedobjects.GetComponent<BaseProperty>().weight.ToString();
        if (_editplacedobjects.GetComponent<MotorProperty>() != null)
        {
            setdurationtinputfield.text = _editplacedobjects.GetComponent<MotorProperty>().duration.ToString();
            settrustinputfield.text = _editplacedobjects.GetComponent<MotorProperty>().force.ToString();
        }
        _selectedobject.GetComponent<Outline>().enabled = true;
        UnHide();
        if (_selectedobject.GetComponent<MotorProperty>() == null)
        {
            motorparmeditor.SetActive(false);
        }
        HideLaunchButton();
    }

    private void HideLaunchButton()
    {
        launchbutton.SetActive(false);
    }

    private void ShowLaunchButton()
    {
        launchbutton.SetActive(true);
    }
    public void ToggleMoveObject()
    {
        if (Selectedobject != null && _editplacedobjects.CanPlace)
        {
            if (CheckConnections())
            {
                _editplacedobjects.ismouving = !_editplacedobjects.ismouving;
            }
        }
        else
        {
            Debug.Log("Cannot move the object. Please select object.");
        }
    }

    public void DeleteObject()
    {
        if(CheckConnections())
        {
            if (Selectedobject != null)
            {
                physicengine.weight -= Selectedobject.GetComponent<BaseProperty>().weight;
                if (Selectedobject.GetComponent<MotorProperty>() == null)
                {
                    physicengine.RegisterObject(Selectedobject);
                }
                else
                {
                    physicengine.RemoveMotor(Selectedobject);
                }
                Destroy(Selectedobject);
            }
            Selectedobject = null;
            Hide();
        }
        
    }
    
    private void Hide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void UnHide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void SetWeight(string text)
    {
        _selectedobject.GetComponent<BaseProperty>().weight = float.Parse(text);
    }

    public void SetMotorForce(string text)
    {
        _selectedobject.GetComponent<MotorProperty>().force = float.Parse(text);
    }

    public void SetMotorDuration(string text)
    {
        _selectedobject.GetComponent<MotorProperty>().duration = float.Parse(text);
    }

    private bool CheckConnections()
    {
        if(_editplacedobjects.canplaceobject.Count > 1)
        {
            Transform starting;
            if(Selectedobject.transform.parent.GetChild(0) != Selectedobject.transform)
            {
                starting = Selectedobject.transform.parent.GetChild(0);
            }
            else if (Selectedobject.transform.parent.transform.childCount == 1)
            {
                return true;
            }
            else
            {
                starting = Selectedobject.transform.parent.GetChild(1);
            }
            List<Transform> Connections = new List<Transform>();
            Connections.Add(starting);
            List<Transform> CheckList = new List<Transform>();
            CheckList.Add(starting);
            while (CheckList.Count != 0)
            {
                        foreach (Collider collider in CheckList[0].GetComponent<MoveObjects>().canplaceobject.Keys)
                        {
                            if (collider != null)
                            {
                                if(!Connections.Contains(collider.gameObject.transform) && collider.gameObject != Selectedobject)
                                {
                                    Connections.Add(collider.gameObject.transform);
                                    CheckList.Add(collider.gameObject.transform);
                                }
                            }
                            
                        }
                        CheckList.Remove(CheckList[0]);
            }

            if (Connections.Count < Selectedobject.transform.parent.childCount-1)
            {
                return false;
            }
        }

        return true;
    }
}
