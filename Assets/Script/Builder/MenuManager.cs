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
    public TextMeshProUGUI priceForObjects;
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
    public floatscriptableobject radius;
    public floatscriptableobject PriceGoal;
    public boolvariable Challenges;
    public TextMeshProUGUI PriceShowing;

    private void Start()
    {
        if(Challenges.value)
        {
            PriceShowing.gameObject.SetActive(true);
            PriceShowing.text = "Prix max :" + PriceGoal.value + "$";
        }
        
        slider_x.onValueChanged.AddListener((v) =>
        {
            if (Selectedobject != null)
                {
                if (CheckConnections())
                {
                    Vector3 localScale = Selectedobject.transform.localScale;
                    BaseProperty baseproperty = Selectedobject.GetComponent<BaseProperty>();
                    if (Selectedobject.GetComponent<MotorProperty>() == null)
                    {
                        localScale = new Vector3(v*localScale.x/baseproperty.last_x,localScale.y,localScale.z);
                    }
                    else
                    {
                        localScale = new Vector3(v*localScale.x/baseproperty.last_x,localScale.y,localScale.z);
                    }
                    Selectedobject.transform.localScale = localScale;
                    if (v != baseproperty.last_x)
                    {
                        Selectedobject.GetComponent<MoveObjects>().ismouving = true;
                    }
                    baseproperty.last_x = v;
                    if(baseproperty.last_x % 2 == 0)
                    {
                        baseproperty.decalement_x = 0.5f;
                    }
                    else
                    {
                        baseproperty.decalement_x = 0;
                    }
                    Selectedobject.transform.localScale = localScale;
                    Selectedobject.GetComponent<MoveObjects>().UpdateScale();
                    radius.value = localScale.x / 2;
                }
            }
        });
        slider_z.onValueChanged.AddListener((v) =>
        {
            if (Selectedobject != null)
            {
                if (CheckConnections())
                {
                    BaseProperty baseproperty = Selectedobject.GetComponent<BaseProperty>();
                    Vector3 localscale = Selectedobject.transform.localScale;
                    if(Selectedobject.name == ("Cube(Clone)") || Selectedobject.name == ("Cylinder_test(Clone)"))
                    {
                        localscale = new Vector3(localscale.x,localscale.y,v*localscale.z/baseproperty.last_z);
                        if (v != baseproperty.last_z)
                        {
                            Selectedobject.GetComponent<MoveObjects>().ismouving = true;
                        }
                        baseproperty.last_z = v;
                    }
                    else
                    {
                        localscale = new Vector3(localscale.x,v*localscale.y/baseproperty.last_z,localscale.z);
                        if (v != baseproperty.last_z)
                        {
                            Selectedobject.GetComponent<MoveObjects>().ismouving = true;
                        }
                        baseproperty.last_z = v;
                    }
                    if(baseproperty.last_z % 2 == 0)
                    {
                        baseproperty.decalement_z = 0.5f;
                    }
                    else
                    {
                        baseproperty.decalement_z = 0;
                    }
                    Selectedobject.transform.localScale = localscale;
                    Selectedobject.GetComponent<MoveObjects>().UpdateScale();
                }
            }
        });
        slider_y.onValueChanged.AddListener((v) =>
        {
            if (Selectedobject != null)
            {
                if (CheckConnections())
                {
                    BaseProperty baseproperty = Selectedobject.GetComponent<BaseProperty>();
                    Vector3 localscale = Selectedobject.transform.localScale;
                    if(Selectedobject.name == ("Cube(Clone)") || Selectedobject.name == ("Cylinder_test(Clone)"))
                    {
                        localscale = new Vector3(localscale.x,v*localscale.y/baseproperty.last_y,localscale.z);
                        if (v != baseproperty.last_y)
                        {
                            Selectedobject.GetComponent<MoveObjects>().ismouving = true;
                        }
                        baseproperty.last_y = v;
                    }
                    else
                    {
                        localscale = new Vector3(localscale.x,localscale.y,localscale.z*v/baseproperty.last_y);
                        if (v != baseproperty.last_y)
                        {
                            Selectedobject.GetComponent<MoveObjects>().ismouving = true;
                        }
                        baseproperty.last_y = v;
                    }
                    if(baseproperty.last_y % 2 == 0)
                    {
                        baseproperty.decalement_y = 0.5f;
                    }
                    else
                    {
                        baseproperty.decalement_y = 0;
                    }
                    Selectedobject.transform.localScale = localscale;
                    Selectedobject.GetComponent<MoveObjects>().UpdateScale();
                }
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

        if (Selectedobject != null)
        {
            if (Selectedobject.GetComponent<MotorProperty>() != null)
            {
                float price =  (5 * Selectedobject.GetComponent<MotorProperty>().duration *
                            Selectedobject.GetComponent<MotorProperty>().force)/(Selectedobject.GetComponent<BaseProperty>().weight *
                                   (Selectedobject.GetComponent<BaseProperty>().last_x *
                                    Selectedobject.GetComponent<BaseProperty>().last_z *
                                    Selectedobject.GetComponent<BaseProperty>().last_y));
                if(price > 0)
                {
                    Selectedobject.GetComponent<BaseProperty>().price = price;
                    priceForObjects.text = Selectedobject.GetComponent<BaseProperty>().price.ToString() + "$";
                }
                //Debug.Log(Selectedobject.GetComponent<BaseProperty>().price);
            }   
        }

    }
    public void UnSelectObject()
    {
        _editplacedobjects = _selectedobject.GetComponent<MoveObjects>();
        if (!_editplacedobjects.CanPlace)
        {
            return;
        }
        _editplacedobjects.ismouving = false;
        _selectedobject.GetComponent<Outline>().enabled = false;
        _selectedobject = null;
        Hide();
        ShowLaunchButton();
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
        if (_editplacedobjects.GetComponent<MotorProperty>() != null)
        {
            slider_x.gameObject.SetActive(false);
            slider_y.gameObject.SetActive(false);
            slider_z.gameObject.SetActive(false);
        }

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
