using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject parent;
    private GameObject objectloading;
    
    public void SelectObject(int index)
    {
        if (objectloading != null)
        {
            if (objectloading.GetComponent<EditPlacedObjects>().IsSelected)
            {
                Destroy(objectloading);
            }
        }
        objectloading = Instantiate(objects[index], new Vector3(0,0,0), objects[index].transform.rotation, parent.transform);
    }
}