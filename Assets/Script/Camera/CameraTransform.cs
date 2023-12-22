using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraTransform", menuName = "ScriptableObjects/CameraTransform", order = 1)]
public class CameraTransform : ScriptableObject
{
    [SerializeField] public Vector3 position;
    [SerializeField] public Vector3 rotation;
    [SerializeField] public Vector3 scale;
}
