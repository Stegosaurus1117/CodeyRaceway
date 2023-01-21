using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinishLine : MonoBehaviour
{
    public CheckpointCounter checkpointTracker;
    public Rigidbody avatar;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (checkpointTracker.triggeredCheckpoints == checkpointTracker.numberOfCheckpoints)
            {
                Debug.Log("You Win!");
            }
            else
            {
                print("Cheater!");
                //avatar.mass = 1;
                //avatar.drag = 0;
                //avatar.AddForce(1000, 5000, 3000, ForceMode.Impulse);
                
                
            }
            
        }
    }
}
