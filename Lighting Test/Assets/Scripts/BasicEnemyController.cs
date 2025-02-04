using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class BasicEnemyController : MonoBehaviour
{
    [Header("Basic Stats")]
    public int health = 5;
    public int maxHealth = 5;
    public bool canMove = true;
    public bool canTakeDamage = true;

    public PlayerController Player;
    public NavMeshAgent agent;
    public GameObject detection;
    public TextMeshPro healthText;



    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        healthText.text = "Health: " + health;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
            Destroy(detection);
        }

        if (detection.GetComponent<BasicEnemyDetectionBox>().detectPlayer == true && canMove == true && health >= 0)
        {
            agent.destination = Player.transform.position;
        } 

        if(health >= 0)
        {
           healthText.text = "Health: " + health;
           detection.transform.position = gameObject.transform.position;
        }
           
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "HealthRegenLight" && canTakeDamage == true)
        {
            health--;
            canTakeDamage = false;
            StartCoroutine("HitCoolDown");
        }
    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(2f);
        canTakeDamage = true;
    }
}