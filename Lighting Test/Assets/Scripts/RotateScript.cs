using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float rotationSpeed = 1;
    public GameObject spotLight;

    // Update is called once per frame
    void Update()
    {
       
        
    }

    private void OnTriggerStay(Collider other)
    {
        StartCoroutine("Rotate");
    }

    IEnumerator Rotate()
    {
        yield return new WaitForSeconds(0.5f);
        spotLight.transform.Rotate(new Vector3(0, rotationSpeed, 0), Space.World);
    }
}
