using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickControls : MonoBehaviour
{
    public GameObject[] robotGameObjects;
    public Camera[] cam;
    public CameraController CameraControllerScript;
    public AudioSource changeRobot;

    private int _selectedRobot = 0;
    private RaycastHit _hit;
    private RaycastHit2D _hit2D;
    private Ray _ray;
    private NavMeshHit _navMeshHit;
    private GraphicRaycaster _raycaster;
    private bool _hitCanvas = false;

    void Start()
    {
        robotGameObjects[_selectedRobot].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        robotGameObjects[_selectedRobot].GetComponent<Move>().selected = true;
        _raycaster = GetComponent<GraphicRaycaster>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            pointerData.position = Input.mousePosition;
            if (_raycaster)
            {
                _raycaster.Raycast(pointerData, results);

                foreach (RaycastResult ressie in results)
                {
                    _hitCanvas = true;
                }
                if (_hitCanvas == false)
                {
                    _ray = cam[CameraControllerScript.roomNo].ScreenPointToRay(Input.mousePosition);
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
                _hitCanvas = false;
            }
        }
    }

    /*public void NextRobot()
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
    }*/

    public void SwapRobots(int i)
    {
        _selectedRobot = i;
        changeRobot.Play();
        foreach (GameObject rob in robotGameObjects)
        {
            rob.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            rob.GetComponent<Move>().selected = false;
        }
        robotGameObjects[_selectedRobot].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        robotGameObjects[_selectedRobot].GetComponent<Move>().selected = true;
    }
}
