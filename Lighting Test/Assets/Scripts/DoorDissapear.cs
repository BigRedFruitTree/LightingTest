using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDissapear : MonoBehaviour
{
    public GameObject directionLight;
    public GameObject door;
    private float rotationDetect = 194.006f;

    // Start is called before the first frame update
    void Start()
    {
        directionLight = GameObject.Find("Directional Light");
    }

    // Update is called once per frame
    void Update()
    {
        if(directionLight.transform.rotation == Quaternion.Euler(rotationDetect, 0, 0))
        {
            door.SetActive(false);
        }
        else
        {
            door.SetActive(true);
        }
    }
}
