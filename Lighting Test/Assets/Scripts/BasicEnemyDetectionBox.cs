using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyDetectionBox : MonoBehaviour
{

    public bool detectPlayer = false;
    public GameObject detectionBox;
    public BasicEnemyController controller;

    // Start is called before the first frame update
    void Start()
    {
        detectionBox = GameObject.Find("Detection");
    }

    // Update is called once per frame
    void Update()
    {
    
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            detectPlayer = true;
            controller.canMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            detectPlayer = false;
            controller.canMove = false;
        }
    }
}