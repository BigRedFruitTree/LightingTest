using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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



    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if (detection.GetComponent<BasicEnemyDetectionBox>().detectPlayer == true && canMove == true)
        {
            agent.destination = Player.transform.position;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "HealthRegenLight" && canTakeDamage == true)
        {
           health--;
           canTakeDamage = false;
           StartCoroutine("HitCoolDown");
        }
    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
    }
}