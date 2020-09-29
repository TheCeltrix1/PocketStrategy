using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingStation : MonoBehaviour
{
    private float _time;

    private void Update()
    {
        _time += Time.deltaTime;
    }
}
