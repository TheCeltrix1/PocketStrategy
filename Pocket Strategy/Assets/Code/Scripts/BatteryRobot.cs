using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatteryRobot : MonoBehaviour
{
    private NavMeshAgent _navMeshMoveComponent;
    private Move _moveComponent;
    private SphereCollider _sphere;
    private ArrayList _nearbyRobots = new ArrayList();

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
        //Debug.Log(_nearbyRobots.Count);
        Recharge();
    }

    void Recharge()
    {
        if (Input.GetButton("ActiveAbility"))
        {
            if (_nearestObject.GetComponent<Move>().powerReserves < _nearestObject.GetComponent<Move>().powerReservesMax)
            {
                _moveComponent.powerReserves -= 10;
                _nearestObject.GetComponent<Move>().powerReserves += 10;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Move>())
        {
            _nearbyRobots.Add(other);
        }
        if (_nearestObject == null)
        {
            _nearestObject = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach(Object obj in _nearbyRobots)
        {
            if (Vector3.Distance(this.transform.position, other.transform.position) < Vector3.Distance(this.transform.position, _nearestObject.transform.position))
            {
                _nearestObject = other.gameObject;
                Debug.Log(_nearestObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Move>())
        {
            _nearbyRobots.Remove(other);
        }
        if (_nearbyRobots.Count == 0)
        {
            _nearestObject = null;
        }
    }
}
