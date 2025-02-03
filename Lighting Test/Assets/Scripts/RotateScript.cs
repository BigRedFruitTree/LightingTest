using System.Collections;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float rotationSpeedY = 1;
    public float rotationSpeedX = 0;
    public float rotationSpeedZ = 0;
    public float rotateSpeedDelay = 0.5f;
    public GameObject spotLight;
    public PlayerController playerController;
    public float detectDistance = 1.5f;
    public RaycastHit hit;
    public Ray ray;
    public LayerMask whatIsTargetable;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, detectDistance, whatIsTargetable) && playerController.canTakeDamage == true)
        {
            playerController.canTakeDamage = false;
            playerController.health--; 
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        StartCoroutine("Rotate");
    }

    IEnumerator Rotate()
    {
        yield return new WaitForSeconds(rotateSpeedDelay);
        spotLight.transform.Rotate(new Vector3(rotationSpeedX, rotationSpeedY, rotationSpeedZ), Space.World);
    }
}
