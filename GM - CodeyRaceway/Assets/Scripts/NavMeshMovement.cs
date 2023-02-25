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
        
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        PotTargets = new GameObject[GameObject.FindGameObjectsWithTag("Obstacle").Length;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //Debug.Log(player.position);
    }

    private void findTarget()
    {
        
        
        /*GameObject[] array = GameObject.FindGameObjectsWithTag("obstacle");
        foreach (GameObject obs in array)
        {
            
        }*/
    }

  
}
