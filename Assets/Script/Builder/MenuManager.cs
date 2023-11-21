using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
            _editplacedobjects.ismouving = !_editplacedobjects.ismouving;
        }
        else
        {
            Debug.Log("Cannot move the object. Please select object.");
        }
    }

    public void DeleteObject()
    {
        if (Selectedobject != null)
        {
            Destroy(Selectedobject);
        }
        Selectedobject = null;
        Hide();
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
}
