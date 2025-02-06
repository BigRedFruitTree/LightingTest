using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public GameObject PlayerObject;
    private Rigidbody myRB;
    public Camera playerCam;
    Transform cameraHolder;
    public GameObject ceiling;

    [Header("Shader Stuff")]
    public GameObject postProcessLayer;
    public GameObject postProcessVolume;
    public GameObject postProcessBloom;

    [Header("Light Stuff")]
    public GameObject directionLight;
    public GameObject areaLight1;
    public Light spotLight;
    public Light pointLight;
    public float rotationSpeed = 10f;

    Vector2 camRotation;

    [Header("Player Stats")]
    public int health = 5;
    public int maxHealth = 5;
    public int healthRestore = 5;
    public bool canTakeDamage = true;
    public bool canHeal = true;
    public TextMeshProUGUI Health;

    [Header("Movement Settings")]
    public float speed = 10.0f;
    public float sprintMultiplier = 2f;
    [SerializeField] private float jumpHeight = 4f;
    public float groundDetectDistance = 1.5f;
    public int jumps = 2;
    public int jumpsMax = 2;
    public bool sprintMode = false;
    public bool isGrounded = true;
    public float stamina = 150;
    public float maxStamina = 150;
    public bool canJump = true;


    [Header("User Settings")]
    public float mouseSensitivity = 2.0f;
    public float Xsensitivity = 2.0f;
    public float Ysensitivity = 2.0f;
    public float camRotationLimit = 90f;
    public bool GameOver = false;
    public Transform weaponSlot;
    public GameObject FlashlightLight;
    private bool hasFlashlight = false;
   
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        playerCam = Camera.main;
        cameraHolder = transform.GetChild(0);
        camRotation = Vector2.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        playerCam.transform.position = cameraHolder.position;

        if (GameOver == false)
        {

            camRotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
            camRotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

            camRotation.y = Mathf.Clamp(camRotation.y, -camRotationLimit, camRotationLimit);


            playerCam.transform.rotation = Quaternion.Euler(-camRotation.y, camRotation.x, 0);
            transform.localRotation = Quaternion.AngleAxis(camRotation.x, Vector3.up);


        }

        Vector3 temp = myRB.velocity;
        
        float verticalMove = Input.GetAxisRaw("Vertical");
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && GameOver == false)
        {
            if (stamina > 0)
            {

                sprintMode = true;
            }
            else
                sprintMode = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprintMode = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprintMode = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ceiling.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            ceiling.SetActive(false);
        }

        if (sprintMode == true)
            stamina--;

        if (stamina < 0)
            stamina = 0;

        if (stamina == 0)
            sprintMode = false;

        if (sprintMode == false)
        {
            stamina++;
        }

        if (stamina > maxStamina)
            stamina = maxStamina;

        if (!sprintMode)
            temp.x = verticalMove * speed;

        if (sprintMode)
        {
            temp.x = verticalMove * speed * sprintMultiplier;
            temp.z = horizontalMove * speed * sprintMultiplier;
        } else
        {
            temp.x = verticalMove * speed;
            temp.z = horizontalMove * speed;
        }
            
        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0 && GameOver == false && canJump == true)
        {
            temp.y = jumpHeight;
            jumps--;
        }

        myRB.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);

        if (health < 0)
            health = 0;

        if (health == 0)
            GameOver = true;

        if (canTakeDamage == false)
            StartCoroutine("HitCoolDown");

        if(health > maxHealth)
        health = maxHealth;

        Health.text = "Health: " + health;

        if (Input.GetMouseButtonDown(0) && hasFlashlight == true)
        {
           FlashlightLight.SetActive(true);
        }
        
        if (Input.GetMouseButtonUp(0) && hasFlashlight == true)
        {
           FlashlightLight.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumps = jumpsMax;
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Enemy" && canTakeDamage == true)
        {
            health--;
            canTakeDamage = false;
            StartCoroutine("HitCoolDown");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Direction Light Rotator")
        {
            rotationSpeed = 10f;
            directionLight.transform.Rotate(new Vector3(rotationSpeed, 0, 0), Space.World);
        }
        if (other.gameObject.name == "Direction Light Reset")
        {
            rotationSpeed = 94.00598f;
            directionLight.transform.rotation = Quaternion.Euler(rotationSpeed, 0, 0);
        }

        if (other.gameObject.name == "Spot Light Range Modifier +")
        {
            spotLight.range++;
        }
        if (other.gameObject.name == "Spot Light Range Modifier -")
        {
            spotLight.range--;
        }
        if (other.gameObject.name == "Spot Light intensity Modifier + ")
        {
            spotLight.intensity++;
        }
        if (other.gameObject.name == "Spot Light intensity Modifier - ")
        {
            spotLight.intensity--;
        }

        if (other.gameObject.name == "Point Light Range Modifier +")
        {
            pointLight.range++;
        }
        if (other.gameObject.name == "Point Light Range Modifier -")
        {
            pointLight.range--;
        }
        if (other.gameObject.name == "Point Light intensity Modifier +")
        {
            pointLight.intensity++;
        }
        if (other.gameObject.name == "Point Light intensity Modifier -")
        {
            pointLight.intensity--;
        }

        if (other.gameObject.name == "Area Light Rotator z")
        {
            float rotationSpeed = 10f;
            areaLight1.transform.Rotate(new Vector3(0, 0, rotationSpeed), Space.World);
        }
        if (other.gameObject.name == "Area Light Rotator x")
        {
            float rotationSpeed = 10f;
            areaLight1.transform.Rotate(new Vector3(rotationSpeed, 0, 0), Space.World);
        }
        if (other.gameObject.name == "Area Light Rotator y")
        {
            float rotationSpeed = 10f;
            areaLight1.transform.Rotate(new Vector3(0, rotationSpeed, 0), Space.World);
        }

        if (other.gameObject.name == "Extra Jump Light")
        {
            jumpHeight = 12;
        }

        if (other.gameObject.name == "Flashlight")
        {
            hasFlashlight = true;
            other.gameObject.transform.SetPositionAndRotation(weaponSlot.position, weaponSlot.rotation);
            other.gameObject.transform.SetParent(weaponSlot);
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "HealthRegenLight" && canHeal == true)
        {
           canHeal = false;
           health++;
           StartCoroutine("HealCoolDown");
        }

        if (other.gameObject.tag == "NoJumpLight")
        {
          canJump = false;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "NoJumpLight")
        {
          canJump = true;
        }

        if (other.gameObject.name == "Extra Jump Light")
        {
            jumpHeight = 4;
        }
    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
    }

    IEnumerator HealCoolDown()
    {
        yield return new WaitForSeconds(1f);
        canHeal = true;
    }
}