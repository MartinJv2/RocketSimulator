using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ChangeRotation : MonoBehaviour
{
    [SerializeField] public Slider slider_x;
    [SerializeField] public Slider slider_z;
    private float rotation_x;
    private float rotation_y;
    private GameObject _selectedobject;

    private void Select(GameObject value)
    {
        if (_selectedobject != null)
        {
            UnSelect();
        }

        Show();
        _selectedobject = value;
        slider_z.value = _selectedobject.GetComponent<BaseProperty>().last_y_rotation;
        slider_x.value = _selectedobject.GetComponent<BaseProperty>().last_x_rotation;
        _selectedobject.GetComponent<Outline>().enabled = true;
    }

    private void Hide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void Show()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void UnSelect()
    {
        _selectedobject.GetComponent<Outline>().enabled = false;
        _selectedobject = null;
        Hide();
    }

    void Start()
    {
        slider_x.onValueChanged.AddListener((value) =>
        {
            Transform child = _selectedobject.transform.GetChild(2);
            BaseProperty baseproprety = _selectedobject.GetComponent<BaseProperty>();
            baseproprety.last_x_rotation = 90 + value;
            child.rotation = Quaternion.Euler(new Vector3(baseproprety.last_x_rotation, baseproprety.last_y_rotation, 0));
        });
        slider_z.onValueChanged.AddListener((value) =>
        {
            Debug.Log(value);
            Transform child = _selectedobject.transform.GetChild(2);
            BaseProperty baseproprety = _selectedobject.GetComponent<BaseProperty>();
            baseproprety.last_y_rotation = value;
            child.transform.rotation = Quaternion.Euler(new Vector3(baseproprety.last_x_rotation, baseproprety.last_y_rotation, 0));
            
        });
    }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    if (hit.collider.gameObject.GetComponent<MotorProperty>() != null &&
                        hit.collider.gameObject != _selectedobject)
                    {
                        Select(hit.collider.gameObject);
                    }
                    else
                    {
                        UnSelect();
                    }
                    

                }
            }
        }
}
