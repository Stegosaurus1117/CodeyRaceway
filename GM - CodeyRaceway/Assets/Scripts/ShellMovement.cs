using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShellMovement : MonoBehaviour
{
    
   
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 100; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
