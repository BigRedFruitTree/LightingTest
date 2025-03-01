using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLightChanger : MonoBehaviour
{
    public new Light light;
    public SphereCollider dLight;
    public PlayerController Player;
    [SerializeField] public float timer;
    public bool canTimerStart = true;
    public float min = 0f;
    public float max = 9.00001f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        dLight = GetComponent<SphereCollider>();
        timer = 9f;
    }

    // Update is called once per frame
    void Update()
    {

        light.intensity = timer;
        light.range = timer;
        dLight.radius = timer;


        if (Player.inTheThickOfIt == true)
        {
            if(light.intensity <= max && light.intensity != min && canTimerStart == true || light.intensity >= max && light.intensity != min && canTimerStart == true)
            {
                StartCoroutine("Wait");
                timer -= Time.deltaTime;
            } else
            {
                canTimerStart = false;
            }


            if (light.intensity <= max && canTimerStart == false)
            {
                StartCoroutine("Wait");
                timer += Time.deltaTime;
            } else
            {
                canTimerStart = true;
            }

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }
}
