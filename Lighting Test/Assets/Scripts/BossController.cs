using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class BossController : MonoBehaviour
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
    public Light bLight;
    public Light flashLightLight;

    public SphereCollider dLight;
    [SerializeField] public float timer;
    public bool canTimerStart = true;
    public float min = 0f;
    public float max = 9.00001f;

    public bool hit = false;

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
        if (health <= 0)
        {
            Destroy(gameObject);
            Destroy(detection);
        }

        if (detection.GetComponent<BasicEnemyDetectionBox>().detectPlayer == true && canMove == true && health >= 0)
        {
            agent.destination = Player.transform.position;
        }

        if (health >= 0)
        {
            healthText.text = "Health: " + health;
            detection.transform.position = gameObject.transform.position;
        }

        bLight.intensity = timer;
        bLight.range = timer;
        dLight.radius = timer;


        if (Player.inTheThickOfIt == true)
        {
            if (bLight.intensity <= max && bLight.intensity != min && canTimerStart == true || bLight.intensity >= max && bLight.intensity != min && canTimerStart == true)
            {
                StartCoroutine("Wait");
                timer -= Time.deltaTime;
            }
            else
            {
                canTimerStart = false;
            }


            if (bLight.intensity <= max && canTimerStart == false)
            {
                StartCoroutine("Wait");
                timer += Time.deltaTime;
            }
            else
            {
                canTimerStart = true;
            }

            if(hit == true && canTakeDamage == true)
            {
                health--;
                canTakeDamage = false;
                StartCoroutine("HitCoolDown");
            }

        }

       
    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(2f);
        canTakeDamage = true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}