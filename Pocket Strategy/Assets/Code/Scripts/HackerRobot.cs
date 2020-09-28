using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HackerRobot : MonoBehaviour
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

        _moveComponent.powerReservesMax = 500;
        _moveComponent.powerReserves = 500;

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
        if (_selected) Hack();
    }

    void Hack()
    {
        if (Input.GetButton("ActiveAbility"))
        {
            if (_nearestObject.GetComponent<Hackable>())
            {
                _nearestObject.GetComponent<Hackable>().CommenceHack();
            }
        }
    }
}
