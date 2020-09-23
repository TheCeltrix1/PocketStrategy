using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    public bool moveToPosition = false;
    public Vector3 desitination;
    public int speed;

    private NavMeshAgent _navMesh;

    void Start()
    {
        _navMesh = this.transform.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveToPosition == true)
        {
            _navMesh.Move((desitination - transform.position) * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position,desitination) < 1)
        {
            moveToPosition = false;
        }
    }
}
