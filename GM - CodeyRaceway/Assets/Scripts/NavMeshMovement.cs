using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : MonoBehaviour
{
    public Transform goal;
    private NavMeshAgent agent;
    public GameObject player;
    public GameObject[] PotTargets;
    // Start is called before the first frame update
    private void Start()
    {
        
        PotTargets = GameObject.FindGameObjectsWithTag("Obstacle");
        agent = GetComponent<NavMeshAgent>();

        ChangeObs();

       
        InvokeRepeating("ChangeObs()", 1f, 2f);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        
    }

    private void findTarget()
    {
        

        //GameObject[] array = GameObject.FindGameObjectsWithTag("obstacle");
        for (int i = 0; i < PotTargets.Length; i++)
        {
           
        }
    }

    private void ChangeObs()
    {
        int randomobs = Random.Range(0, PotTargets.Length - 1);
        goal = PotTargets[randomobs].transform;
        agent.destination = goal.position;
    }

  
}
