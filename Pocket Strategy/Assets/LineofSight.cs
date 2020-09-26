using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LineofSight : MonoBehaviour
{
    public GameObject lestSeenLocation;
    public GameObject player;

    public Transform eyes;
    public bool spotted;
    public bool alert;
    private float _searchTimer;
    public float maxSearchTimer;
    
    public LayerMask obstacleLayers;

    public SpriteRenderer alertLevelSprite;

    private Vector3 _startPos;
    public Vector3 destination;
    private NavMeshAgent _navMesh;
    
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
        }
        else
        {
            Debug.DrawRay(eyes.position, player.transform.position - eyes.position, Color.white);
            spotted = true;
            _searchTimer = maxSearchTimer;
            lestSeenLocation.transform.position = player.transform.position;
        }

        
        // Alert check
        if (spotted)
        {
            alert = true;
            alertLevelSprite.enabled = true;
            alertLevelSprite.color = Color.red;
        }
        else if (!spotted && _searchTimer > 0)
        {
            alertLevelSprite.color = Color.yellow;
            _searchTimer -= 1 * Time.deltaTime;
        }
        else if (_searchTimer <= 0)
        {
            alert = false;
            alertLevelSprite.enabled = false;
        }
        
        
        // Movement
        if (alert)
        {
            destination = lestSeenLocation.transform.position;
            
        }
        else
        {
            destination = _startPos;
            //_navMesh.SetDestination(destination);
        }
        _navMesh.SetDestination(destination);
    }
}
