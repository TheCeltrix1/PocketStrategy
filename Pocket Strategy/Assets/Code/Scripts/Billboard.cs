using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera[] cam;
    public CameraController CameraControllerScript;
    
    void Update()
    {
        transform.LookAt(cam[CameraControllerScript.roomNo].transform.position, Vector3.up);
    }
}
