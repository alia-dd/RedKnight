using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activate_boundary : MonoBehaviour
{
    public GameObject boundary10;
    public GameObject Square_lock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {  
            boundary10.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {  
            Square_lock.SetActive(true);
        }
        
    }
}
