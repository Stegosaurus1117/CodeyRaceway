using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomPowerup : MonoBehaviour
{
    public List<GameObject> powerupList;
    public int randomNumberInList;
    public GameObject chosenPowerup;
    public bool collected = false;
    public CodeyMove CM;
    public TrailRenderer tr;
    
    
    

    Rigidbody rb;
    
    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && collected)
        {
            UsePowerup();
        }
    }

    private void UsePowerup()
    {
        Vector3 vel = rb.velocity;
        Vector3 orange = transform.position + (vel.normalized * 10);

        switch (randomNumberInList)
        {
            case 0:
            Instantiate(chosenPowerup, orange, transform.rotation);
                break;
            case 1:
            Instantiate(chosenPowerup, orange, transform.rotation);
                break;
            case 2:
                CM.Speed = 1000;
                tr.enabled = true;
                Invoke("NormalizeSpeed", 5f);
                break;
            
        }

        if (collected)
        {
            collected = false;
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

    private void NormalizeSpeed()
    {
        CM.Speed = 80;
        tr.enabled = false;
    }
    


}
