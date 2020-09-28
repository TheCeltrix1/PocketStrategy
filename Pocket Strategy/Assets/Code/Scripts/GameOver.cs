using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public float alertlevel;
    private float time;
    public int robots;

    private void Start()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Move>())
        {
            //Time.timeScale = 0;
        }
    }
}
