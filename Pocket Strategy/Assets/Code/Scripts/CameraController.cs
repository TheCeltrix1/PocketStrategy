using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int roomNo;

    public GameObject[] cameraHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (roomNo == 0)
        {
            cameraHolder[0].SetActive(true);
            cameraHolder[1].SetActive(false);
            cameraHolder[2].SetActive(false);
            cameraHolder[3].SetActive(false);
        }
        else if (roomNo == 1)
        {
            cameraHolder[1].SetActive(true);
            cameraHolder[0].SetActive(false);
            cameraHolder[2].SetActive(false);
            cameraHolder[3].SetActive(false);
        }
        else if (roomNo == 2)
        {
            cameraHolder[2].SetActive(true);
            cameraHolder[0].SetActive(false);
            cameraHolder[1].SetActive(false);
            cameraHolder[3].SetActive(false);
        }
        else if (roomNo == 3)
        {
            cameraHolder[3].SetActive(true);
            cameraHolder[0].SetActive(false);
            cameraHolder[1].SetActive(false);
            cameraHolder[2].SetActive(false);
        }
    }
}
