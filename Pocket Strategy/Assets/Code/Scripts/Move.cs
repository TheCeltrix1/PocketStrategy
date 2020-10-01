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
    public bool escaped;

    public float powerReserves;
    public float powerReservesMax;
    public bool selected;
    public GameObject nearestObject;
    
    public AudioSource death;

    private bool _audioNotPlayed = true;
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
        if (powerReserves <= 0)
        {
            powerReserves = 0;
            if (selected && _audioNotPlayed)
            {
                _audioNotPlayed = false;
                death.Play();
            }
            destination = this.transform.position;
        }
        else
        {
            _audioNotPlayed = true;
        }
        _navMesh.SetDestination(destination);
        if ((Mathf.Abs(_navMesh.velocity.x) > 1f || Mathf.Abs(_navMesh.velocity.z) > 1f) && selected)
        {
            PowerChange(-25);
        }
        if (selected)
        {
            _batterySlider.GetComponent<Slider>().value = powerReserves;
            if (Input.GetButton("ActiveAbility"))
            {
                if (nearestObject.GetComponent<ChargingStation>())
                {
                    PowerChange(50);
                }
            }
        }
    }

    void Interact()
    {
    }

    public void PowerChange(float valueChange)
    {
        powerReserves += (valueChange * Time.deltaTime);
        if (powerReserves > powerReservesMax)
        {
            powerReserves = powerReservesMax;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Move>() || other.GetComponent<Hackable>() || other.GetComponent<ChargingStation>())
        {
            _nearbyRobots.Add(other);
        }
        if (nearestObject == null && (other.GetComponent<Move>() || other.GetComponent<Hackable>() || other.GetComponent<ChargingStation>()))
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
        if (other.GetComponent<Move>() || other.GetComponent<Hackable>() || other.GetComponent<ChargingStation>())
        {
            _nearbyRobots.Remove(other);
        }
        if (_nearbyRobots.Count == 0)
        {
            nearestObject = null;
        }
    }
}
