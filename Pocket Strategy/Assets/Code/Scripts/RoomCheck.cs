using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCheck : MonoBehaviour
{
    public CameraController CameraControllerScript;
    public Move parentActive;
    private RaycastHit _rayTest;
    
    /*private void OnTriggerStay(Collider other)
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
    }*/

    private void Update()
    {
        if (parentActive.selected) {
            Physics.Raycast(this.transform.position, Vector3.down, out _rayTest);
            if (_rayTest.transform.CompareTag("RoomOne"))
                CameraControllerScript.roomNo = 0;
            else if (_rayTest.transform.CompareTag("RoomTwo"))
                CameraControllerScript.roomNo = 1;
            else if (_rayTest.transform.CompareTag("RoomThree"))
                CameraControllerScript.roomNo = 2;
            else if (_rayTest.transform.CompareTag("RoomFour"))
                CameraControllerScript.roomNo = 3;
        }
    }
}
