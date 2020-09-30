using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCheck : MonoBehaviour
{
    public CameraController CameraControllerScript;
    public Move parentActive;
    
    private void OnTriggerStay(Collider other)
    {
        if (parentActive.selected)
        {
            if (other.CompareTag("RoomOne"))
                CameraControllerScript.roomNo = 0;
            else if (other.CompareTag("RoomTwo"))
                CameraControllerScript.roomNo = 1;
            else if (other.CompareTag("RoomThree"))
                CameraControllerScript.roomNo = 2;
            else if (other.CompareTag("RoomFour"))
                CameraControllerScript.roomNo = 3;
        }
        
    }
}
