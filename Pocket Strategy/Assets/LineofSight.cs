using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LineofSight : MonoBehaviour
{
    public GameObject lestSeenLocation;
    public GameObject player;

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
    
    // Start is called before the first frame update
    void Start()
    {
        lestSeenLocation.transform.parent = null;
        
        _navMesh = transform.GetComponent<NavMeshAgent>();
        _navMesh.speed = 2;
        _navMesh.acceleration = 20;
        _navMesh.angularSpeed = 90;
        destination = transform.position;
        _startPos = destination;

        _scanningTime = 0;
        _scanningTimeMax = 1;
        _distCheck = 1.1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Eyes
        RaycastHit hit;
        
        if (Physics.Raycast(eyes.position, player.transform.position - eyes.position, out hit, 50, obstacleLayers))
        {
            Debug.DrawRay(eyes.position, player.transform.position - eyes.position, Color.yellow);
            spotted = false;
            _distCheck = 1.1f;
            _scanningTime = 0;
        }
        else
        {
            Debug.DrawRay(eyes.position, player.transform.position - eyes.position, Color.white);
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
            lestSeenLocation.transform.position = player.transform.position;
        }

        
        // Movement
        _distanceToTarget = Vector3.Distance(transform.position, destination);
        Debug.Log(_distanceToTarget);
        
        if (alert)
        {
            destination = lestSeenLocation.transform.position;
        }
        else
        {
            destination = _startPos;
            lestSeenLocation.transform.position = new Vector3(0, -10, 0);
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
        }
        
        
        
    }
}
