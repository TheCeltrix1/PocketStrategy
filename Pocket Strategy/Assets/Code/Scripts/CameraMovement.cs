using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public ScriptableObjects CameraMovementInfo;
    
    void Start()
    {
        CameraMovementInfo.robotSelected = 0;
    }

    void Update()
    {
        if (CameraMovementInfo.robotSelected > CameraMovementInfo.robots.Length) CameraMovementInfo.robotSelected = 0;
        else if (CameraMovementInfo.robotSelected < 0) CameraMovementInfo.robotSelected = CameraMovementInfo.robots.Length;
    }
}
