using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatteryRobot : MonoBehaviour
{
    private bool _selected;
    private NavMeshAgent _navMeshMoveComponent;
    private Move _moveComponent;
    private SphereCollider _sphere;
    private float _objectDistance = 10;
    private GameObject _nearestObject;

    void Awake()
    {
        _navMeshMoveComponent = this.GetComponent<NavMeshAgent>();
        _navMeshMoveComponent.speed = 2;
        _navMeshMoveComponent.acceleration = 5;

        _moveComponent = this.GetComponent<Move>();

        _moveComponent.powerReservesMax = 1000;
        _moveComponent.powerReserves = 1000;

        if (this.GetComponent<SphereCollider>() == false)
        {
            this.gameObject.AddComponent<SphereCollider>();
        }
        _sphere = this.gameObject.GetComponent<SphereCollider>();
        _sphere.radius = 2;
        _sphere.isTrigger = true;
    }

    private void Update()
    {
        _nearestObject = _moveComponent.nearestObject;
        if (this.GetComponent<Move>()) _selected = this.GetComponent<Move>().selected;
        else Destroy(this);
        if(_selected) Recharge();
    }

    void Recharge()
    {
        if (Input.GetButton("ActiveAbility"))
        {
            if (_nearestObject.GetComponent<Move>()) {
                if (_nearestObject.GetComponent<Move>().powerReserves < _nearestObject.GetComponent<Move>().powerReservesMax)
                {
                    _moveComponent.PowerChange(30);
                    _nearestObject.GetComponent<Move>().PowerChange(30);
                }
            }
        }
    }
}
