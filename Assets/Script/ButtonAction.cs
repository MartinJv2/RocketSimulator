using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{

	public GameObject ObjectClone;
	public GameObject Parent;
	public int x;

	void start()
	{
		x = 0;
	}

	public void OnButtonPress()
	{
		Instantiate(ObjectClone, new Vector3(x, 0, x), Quaternion.identity, Parent.transform);
		++x;
	}
}
