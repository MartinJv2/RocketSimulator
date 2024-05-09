using Unity.VisualScripting;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject prefab;
    public GameObject parent;
    public GameObject menu;
    public floatscriptableobject radius;
    
    public void CreateObject()
    {
        radius.value = prefab.transform.localScale.x/ 2;
        if (menu.GetComponent<MenuManager>().Selectedobject != null)
        {
            if (menu.GetComponent<MenuManager>().Selectedobject.GetComponent<MoveObjects>().ismouving)
            {
                Destroy(menu.GetComponent<MenuManager>().Selectedobject);
            }
            menu.GetComponent<MenuManager>().Selectedobject = null;
        }
        InstatiateObject();
    }
    private void InstatiateObject()
    {
        menu.GetComponent<MenuManager>().Selectedobject = Instantiate(prefab, new Vector3(0,0,0), prefab.transform.rotation, parent.transform);
    }
}