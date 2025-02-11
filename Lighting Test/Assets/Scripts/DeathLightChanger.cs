using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLightChanger : MonoBehaviour
{
    public new GameObject light;
    public PlayerController Player;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }
}
