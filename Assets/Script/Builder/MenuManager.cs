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

        if (!_editplacedobjects.ismouving)
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
        _editplacedobjects = _selectedobject.GetComponent<MoveObjects>();
        setweightinputfield.text = _editplacedobjects.GetComponent<BaseProperty>().weight.ToString();
        _selectedobject.GetComponent<Outline>().enabled = true;
        UnHide();
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
