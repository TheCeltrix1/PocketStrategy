using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LineofSight : MonoBehaviour
{
    public GameObject lastSeenLocation;
    public GameObject[] players;
    public GameObject targetPlayer;

    public Collider eyesCollider;
    public bool targetFound;

    public Transform eyes;
    public bool scanning;
    private float _scanningTime;
    private float _scanningTimeMax;
    public bool spotted;
    public bool alert;
    private float _searchTimer;
    public float maxSearchTimer;

    public Sprite[] spriteList;
    
    public LayerMask obstacleLayers;

    public SpriteRenderer alertLevelSprite;

    private Vector3 _startPos;
    public Vector3 destination;
    private NavMeshAgent _navMesh;
    private float _distanceToTarget;
    private float _distCheck;

    public GameObject enemyRouteHolder;
    public Transform[] enemyRoute;
    private int routeNo;

    private int _enemyState;
    
    private float _eyeMoveTimer;
    private int _eyeMoveModifier;

    void Start()
    {
        // Detach GameObjects
        lastSeenLocation.transform.parent = null;
        enemyRouteHolder.transform.parent = null;
        
        // NavMesh Setup
        _navMesh = transform.GetComponent<NavMeshAgent>();
        _navMesh.speed = 2;
        _navMesh.acceleration = 20;
        _navMesh.angularSpeed = 500;
        destination = transform.position;
        
        // Default State
        _enemyState = 0;
        
        _scanningTime = 0;
        _scanningTimeMax = 2;
        _distCheck = 1.1f;

        _eyeMoveTimer = 0;
        _eyeMoveModifier = 0;
    }
    
    // 0 - Idle
    // 1 - Spotted
    // 2 - Aggro
    // 3 - Searching

    private void Update()
    {
        RaycastHit hit;
        targetPlayer = players[0];
        if (Physics.Raycast(eyes.position, targetPlayer.transform.position - eyes.position, out hit, 50))
        {
            Debug.DrawRay(eyes.position, targetPlayer.transform.position - eyes.position, Color.yellow);
            //Debug.Log(hit.collider.name);
        }

        if (targetFound)
        {
            if (Physics.Raycast(eyes.position, targetPlayer.transform.position - eyes.position, out hit, 50))
            {
                if (_enemyState == 0 && hit.collider.CompareTag("Player"))
                {
                    //Debug.Log("Test");
                    _scanningTime = 0;
                    _enemyState = 1;
                }
            }
        }


        switch (_enemyState)
        {
            case 0:
            {
                alertLevelSprite.enabled = false;
                
                lastSeenLocation.transform.position = new Vector3(0, -10, 0);
                _distCheck = 0.1f;
            
                destination = new Vector3(enemyRoute[routeNo].position.x, transform.position.y, enemyRoute[routeNo].position.z);
            
                if (_distanceToTarget < _distCheck)
                    routeNo = (routeNo + 1) % enemyRoute.Length;

                _navMesh.speed = 2;
                break;
            }
            case 1:
                _navMesh.speed = 0;

                lastSeenLocation.transform.position = targetPlayer.transform.position;
                destination = lastSeenLocation.transform.position;
                
                alertLevelSprite.enabled = true;
                alertLevelSprite.sprite = spriteList[0];
                
                _scanningTime += 1 * Time.deltaTime;
                if (_scanningTime > _scanningTimeMax)
                    _enemyState = 2;
                break;
            case 2:
                if (Physics.Raycast(eyes.position, targetPlayer.transform.position - eyes.position, out hit, 50))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        _distCheck = 3;
                        _navMesh.speed = _distanceToTarget > _distCheck ? 2 : 0;
                        
                        lastSeenLocation.transform.position = targetPlayer.transform.position;
                        _searchTimer = maxSearchTimer;
                        
                        destination = lastSeenLocation.transform.position;
                
                        alertLevelSprite.sprite = spriteList[1];
                    }
                    else
                    {
                        _enemyState = 3;
                    }
                }
                break;
            case 3:
                _navMesh.speed = 2;
                
                _eyeMoveTimer += 1 * Time.deltaTime;

                if (_eyeMoveTimer >= 1)
                {
                    _eyeMoveTimer = 0;
                    _eyeMoveModifier = (_eyeMoveModifier + 1) % 3;
                }
                
                alertLevelSprite.sprite = spriteList[2 + _eyeMoveModifier];
                
                destination = lastSeenLocation.transform.position;

                if (Physics.Raycast(eyes.position, targetPlayer.transform.position - eyes.position, out hit, 50))
                {
                    if (hit.collider.CompareTag("Player"))
                        _enemyState = 2;
                }
                
                _distCheck = 2.2f;
                if (_distanceToTarget <= _distCheck)
                {
                    _searchTimer -= 1 * Time.deltaTime;
                    if (_searchTimer <= 0)
                        _enemyState = 0;
                }

                targetFound = false;
                
                //Debug.Log(_distanceToTarget);
                break;
        }
        
        _distanceToTarget = Vector3.Distance(transform.position, destination);
        _navMesh.SetDestination(destination);
    }
}

/*// Start is called before the first frame update
    void Start()
    {
        lastSeenLocation.transform.parent = null;
        enemyRouteHolder.transform.parent = null;
        
        _navMesh = transform.GetComponent<NavMeshAgent>();
        _navMesh.speed = 2;
        _navMesh.acceleration = 20;
        _navMesh.angularSpeed = 120;
        destination = transform.position;
        _startPos = destination;

        _scanningTime = 0;
        _scanningTimeMax = 2;
        _distCheck = 1.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetFound)
        { 
            // Eyes
            RaycastHit hit;
        
            if (Physics.Raycast(eyes.position, targetPlayer.transform.position - eyes.position, out hit, 50, obstacleLayers))
            {
                Debug.DrawRay(eyes.position, targetPlayer.transform.position - eyes.position, Color.yellow);
                spotted = false;
                _distCheck = 1.1f;
                _scanningTime = 0;
            }
            else
            {
                Debug.DrawRay(eyes.position, targetPlayer.transform.position - eyes.position, Color.white);
                if (_scanningTime < _scanningTimeMax)
                {
                    alertLevelSprite.enabled = true;
                    alertLevelSprite.sprite = spriteList[0];
                    //alertLevelSprite.color = Color.yellow;
                    _scanningTime += 1 * Time.deltaTime;
                }
                else
                {
                    spotted = true;
                }
            
                _distCheck = 3;
                _searchTimer = maxSearchTimer;
                lastSeenLocation.transform.position = targetPlayer.transform.position;
            }
            
            // Movement
            _distanceToTarget = Vector3.Distance(transform.position, destination);
            //Debug.Log(_distanceToTarget);
        
            if (alert)
            {
                destination = lastSeenLocation.transform.position;
            }
            else
            {
                //destination = _startPos;
                lastSeenLocation.transform.position = new Vector3(0, -10, 0);
            }

            if (_distanceToTarget > _distCheck)
            {
                _navMesh.speed = 2;
            }
            else
            {
                _navMesh.speed = 0;
            }

            _navMesh.SetDestination(destination);
            
            // Alert check
            if (spotted)
            {
                alert = true;
                alertLevelSprite.sprite = spriteList[1];
                //alertLevelSprite.enabled = true;
                //alertLevelSprite.color = Color.red;
            }
            else if (!spotted && _searchTimer > 0 && _distanceToTarget <= 1.1f)
            {
                //alertLevelSprite.color = Color.yellow;
                alertLevelSprite.sprite = spriteList[2];
                _searchTimer -= 1 * Time.deltaTime;
            }
            else if (_searchTimer <= 0)
            {
                alert = false;
                alertLevelSprite.enabled = false;
                targetFound = false;
                targetPlayer = null;
            } 
        }
        else
        {
            lastSeenLocation.transform.position = new Vector3(0, -10, 0);
            _distCheck = 0.1f;
            //destination = _startPos;
            destination = new Vector3(enemyRoute[routeNo].position.x, transform.position.y, enemyRoute[routeNo].position.z);
            _distanceToTarget = Vector3.Distance(transform.position, destination);

            if (_distanceToTarget > _distCheck)
            {
                _navMesh.speed = 2;
            }
            else
            {
                _navMesh.speed = 0;
                routeNo = (routeNo + 1) % enemyRoute.Length;
            }

            _navMesh.SetDestination(destination);
        }
    }*/
