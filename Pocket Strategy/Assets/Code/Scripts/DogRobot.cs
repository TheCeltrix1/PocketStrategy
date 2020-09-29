using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogRobot : MonoBehaviour
{
    private bool _selected;
    private NavMeshAgent _navMeshMoveComponent;
    private Move _moveComponent;
    private SphereCollider _sphere;
    private float _objectDistance = 10;
    private GameObject _nearestObject;
    private AudioSource _woof;

    void Awake()
    {
        _woof = GetComponent<AudioSource>();
        _navMeshMoveComponent = this.GetComponent<NavMeshAgent>();
        _navMeshMoveComponent.speed = 4;
        _navMeshMoveComponent.acceleration = 8;

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
        if (_selected) Bark();
    }

    void Bark()
    {
        if (Input.GetButtonDown("ActiveAbility"))
        {
            _woof.Stop();
            _woof.Play();
        }
    }
}
