using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameObject _selectedobject;
    private MoveObjects _editplacedobjects;
    [HideInInspector]
    public GameObject Selectedobject
    {
        get { return _selectedobject;}
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

    public void UnSelectObject()
    {
        _editplacedobjects = _selectedobject.GetComponent<MoveObjects>();
        if (_editplacedobjects.CanPlace)
        {
            _selectedobject.GetComponent<Outline>().enabled = false;
            _selectedobject = null;
        }
        Hide();
    }

    private void SelectObject(GameObject value)
    {
        if (_selectedobject != null)
        {
            UnSelectObject();
        }
        _selectedobject = value;
        _editplacedobjects = _selectedobject.GetComponent<MoveObjects>();
        _selectedobject.GetComponent<Outline>().enabled = true;
        UnHide();
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
}
