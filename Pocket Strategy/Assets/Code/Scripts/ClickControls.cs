using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickControls : MonoBehaviour
{
    public ScriptableObjects robots;
    public GameObject[] robotGameObjects;

    public Camera _cam;

    private int _selectedRobot = 0;
    private RaycastHit _hit;
    private Ray _ray;
    private NavMeshHit _navMeshHit;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            _ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit))
            {
                if (NavMesh.SamplePosition(_hit.point, out _navMeshHit, 2, NavMesh.AllAreas))
                {
                    Move robbie = robotGameObjects[_selectedRobot].GetComponent<Move>();
                    robbie.desitination = _navMeshHit.position;
                    robbie.moveToPosition = true;
                }
            }
        }
        if (Input.GetButtonDown("NextRobot") || Input.GetButtonDown("PreviousRobot")) {
            if (Input.GetButtonDown("NextRobot")) _selectedRobot++;
            else if (Input.GetButtonDown("PreviousRobot")) _selectedRobot --;

            if (_selectedRobot < 0)
            {
                _selectedRobot = robotGameObjects.Length - 1;
            }
            else if (_selectedRobot >= robotGameObjects.Length)
            {
                _selectedRobot = 0;
            }
        }
    }
}
