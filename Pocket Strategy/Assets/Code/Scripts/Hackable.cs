using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hackable : MonoBehaviour
{
    private float _hackTimer;
    private float _hackingTime = 1;
    private Vector3 _originalPosition;
    private bool _hacked;

    public Vector3 doorOpen;
    public GameObject Door;

    private void Start()
    {
        _originalPosition = Door.transform.position;
    }

    private void Update()
    {
        if(_hacked)Door.transform.position = Vector3.Lerp(Door.transform.position, _originalPosition + doorOpen, .25f);
    }

    public void CommenceHack()
    {
        _hackTimer += Time.deltaTime;
        if (_hackTimer >= _hackingTime)
        {
            _hacked = true;
        }
    }
}
