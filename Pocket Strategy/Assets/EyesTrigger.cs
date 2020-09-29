using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesTrigger : MonoBehaviour
{
    public LineofSight LineofSightScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LineofSightScript.targetFound = true;
            LineofSightScript.targetPlayer = other.gameObject;
        }
    }
}
