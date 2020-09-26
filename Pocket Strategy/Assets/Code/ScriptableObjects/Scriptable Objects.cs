using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CameraInfo", order = 1)]
public class ScriptableObjects : ScriptableObject
{
    public int robotSelected;
    public GameObject[] robots;
    public Transform[] positions;
}
