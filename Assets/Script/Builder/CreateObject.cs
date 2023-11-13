using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject prefab;
    public GameObject parent;
    public GameObject menu;
    
    public void CreateObject()
    {
        if (menu.GetComponent<MenuManager>().Selectedobject != null)
        {
            menu.GetComponent<MenuManager>().Selectedobject = null;
        }
        InstatiateObject();
    }
    private void InstatiateObject()
    {
        menu.GetComponent<MenuManager>().Selectedobject = Instantiate(prefab, new Vector3(0,0,0), prefab.transform.rotation, parent.transform);
    }
}