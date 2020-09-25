using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    public bool moveToPosition = false;
    public Vector3 destination;
    public int speed;

    private NavMeshAgent _navMesh;

    void Start()
    {
        _navMesh = this.transform.GetComponent<NavMeshAgent>();
        _navMesh.speed = 7;
        _navMesh.acceleration = 20;
        _navMesh.angularSpeed = 90;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveToPosition == true)
        {
            _navMesh.SetDestination(destination);
        }
        if (Vector3.Distance(transform.position,destination) < .01f)
        {
            moveToPosition = false;
        }
    }
}
