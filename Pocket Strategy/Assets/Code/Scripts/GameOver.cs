using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject[] robots = new GameObject[3];
    public float alertlevel;
    private float _time;
    public GameObject _gameOver;

    private void Start()
    {
        _time += Time.deltaTime;
        robots = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update()
    {
        int _penisSauce = 0;
        foreach (GameObject robbie in robots)
        {
            if (robbie.GetComponent<Move>().powerReserves <=0 || robbie.GetComponent<Move>().escaped)
            {
                _penisSauce++;
            }
            if (_penisSauce >= robots.Length)
            {
                Debug.Log("Saucy");
                Time.timeScale = 0;
                _gameOver.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Move>())
        {
            other.gameObject.GetComponent<Move>().escaped = true;
        }
    }
}
