using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomPowerup : MonoBehaviour
{
    public List<GameObject> powerupList;
    public int randomNumberInList;
    public GameObject chosenPowerup;
    public bool collected = false;
    

    Rigidbody rb;
    
    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && collected)
        {
            Vector3 vel = rb.velocity;
            Vector3 orange = transform.position + (vel.normalized * 10);

            Instantiate(chosenPowerup, orange, transform.rotation);
            if (collected)
            {
                collected = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "itemBox")
        {
            randomNumberInList = Random.Range(0, powerupList.Count);
            chosenPowerup = powerupList[randomNumberInList];
            collected = true;
        }
    }

}
