using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ClickControls : MonoBehaviour
{
    public GameObject[] robotGameObjects;
    public Camera _cam;

    private int _selectedRobot = 0;
    private RaycastHit _hit;
    private Ray _ray;
    private NavMeshHit _navMeshHit;

    void Start()
    {
        robotGameObjects[_selectedRobot].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        robotGameObjects[_selectedRobot].GetComponent<Move>().selected = true;
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
                    robbie.destination = _navMeshHit.position;
                    robbie.moveToPosition = true;
                }
            }
        }
    }

    public void NextRobot()
    {
        robotGameObjects[_selectedRobot].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        robotGameObjects[_selectedRobot].GetComponent<Move>().selected = false;
        _selectedRobot++;
        if (_selectedRobot >= robotGameObjects.Length)
        {
            _selectedRobot = 0;
            robotGameObjects[_selectedRobot].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        } else robotGameObjects[_selectedRobot].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        robotGameObjects[_selectedRobot].GetComponent<Move>().selected = true;
    }

    public void PreviousRobot()
    {
        robotGameObjects[_selectedRobot].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        robotGameObjects[_selectedRobot].GetComponent<Move>().selected = false;
        _selectedRobot--;
        if (_selectedRobot < 0)
        {
            _selectedRobot = robotGameObjects.Length - 1;
            robotGameObjects[_selectedRobot].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }else robotGameObjects[_selectedRobot].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        robotGameObjects[_selectedRobot].GetComponent<Move>().selected = true;
    }

    public void SwapRobots(int i)
    {
        _selectedRobot = i;
        foreach (GameObject rob in robotGameObjects)
        {
            rob.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            rob.GetComponent<Move>().selected = false;
        }
        switch (i) {

            case 0:

                break;

            case 1:

                break;

            case 2:

                break;
        }
    }
}
