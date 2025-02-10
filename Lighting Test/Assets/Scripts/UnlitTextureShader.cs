using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlitTextureShader : MonoBehaviour
{
    public Texture one;
    public Texture two;
    public Texture three;
    public Texture four;
    public Texture five;
    public Texture six;
    public Texture seven;
    public Texture eight;
    public Texture nine;
    public int currentTexture = 1;
    public new Renderer renderer;
    public bool canChange = true;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (currentTexture == 1 && canChange == true)
            {
                canChange = false;
                renderer.material.mainTexture = two;
                currentTexture = 2;
                StartCoroutine("Wait");
            
            }
            if (currentTexture == 2 && canChange == true)
            {
                canChange = false;
                renderer.material.mainTexture = three;
                currentTexture = 3;
                StartCoroutine("Wait");
            }
            if (currentTexture == 3 && canChange == true)
            {
                canChange = false;
                renderer.material.mainTexture = four;
                currentTexture = 4;
                StartCoroutine("Wait");
            }
            if (currentTexture == 4 && canChange == true)
            {
                canChange = false;
                renderer.material.mainTexture = five;
                currentTexture = 5;
                StartCoroutine("Wait");
            }
            if (currentTexture == 5 && canChange == true)
            {
                canChange = false;
                renderer.material.mainTexture = six;
                currentTexture = 6;
                StartCoroutine("Wait");
            }
            if (currentTexture == 6 && canChange == true)
            {
                canChange = false;
                renderer.material.mainTexture = seven;
                currentTexture = 7;
                StartCoroutine("Wait");
            }
            if (currentTexture == 7 && canChange == true)
            {
                canChange = false;
                renderer.material.mainTexture = eight;
                currentTexture = 8;
                StartCoroutine("Wait");
            }
            if (currentTexture == 8 && canChange == true)
            {
                canChange = false;
                renderer.material.mainTexture = nine;
                currentTexture = 9;
                StartCoroutine("Wait");
            }
            if (currentTexture == 9 && canChange == true)
            {
                canChange = false;
                renderer.material.mainTexture = one;
                currentTexture = 1;
                StartCoroutine("Wait");
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        canChange = true;
    }
}
    
