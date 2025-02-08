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
    public Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            
        }
    }
}
