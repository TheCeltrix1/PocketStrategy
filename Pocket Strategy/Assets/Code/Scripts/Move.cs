using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public bool moveToPosition = false;
    public Vector3 destination;
    public int speed;

    public float powerReserves;
    public float powerReservesMax;
    public bool selected;
    public GameObject nearestObject;

    private Component _robotAbility;
    private ArrayList _nearbyRobots = new ArrayList();
    private GameObject _batterySlider;
    private NavMeshAgent _navMesh;

    void Start()
    {
        _batterySlider = GameObject.Find("BatterySlider");
        selected = false;
        _navMesh = this.transform.GetComponent<NavMeshAgent>();
        _navMesh.speed = 7;
        _navMesh.acceleration = 20;
        _navMesh.angularSpeed = 90;
        destination = this.transform.position;
        if (this.GetComponent<BatteryRobot>())
        {
            _robotAbility = this.GetComponent<BatteryRobot>();
        }
        else if (this.GetComponent<HackerRobot>())
        {
            _robotAbility = this.GetComponent<HackerRobot>();
        }
    }

    void Update()
    {
        if (powerReserves <= 0) destination = this.transform.position;
        _navMesh.SetDestination(destination);
        if ((Mathf.Abs(_navMesh.velocity.x) > 1f || Mathf.Abs(_navMesh.velocity.z) > 1f) && selected)
        {
            PowerChange(-1);
        }
        if (selected)
        {
            _batterySlider.GetComponent<Slider>().value = powerReserves;
        }
    }

    void Interact()
    {
        PowerChange(-25);
    }

    void PowerChange(float valueChange)
    {
        powerReserves += valueChange;
        if (powerReserves > powerReservesMax)
        {
            powerReserves = powerReservesMax;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Move>() || other.GetComponent<Hackable>())
        {
            _nearbyRobots.Add(other);
        }
        if (nearestObject == null && (other.GetComponent<Move>() || other.GetComponent<Hackable>()))
        {
            nearestObject = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (Object obj in _nearbyRobots)
        {
            if (Vector3.Distance(this.transform.position, other.transform.position) < Vector3.Distance(this.transform.position, nearestObject.transform.position))
            {
                nearestObject = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Move>() || other.GetComponent<Hackable>())
        {
            _nearbyRobots.Remove(other);
        }
        if (_nearbyRobots.Count == 0)
        {
            nearestObject = null;
        }
    }
}
