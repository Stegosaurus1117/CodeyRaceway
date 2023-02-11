using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBoxSpawner : MonoBehaviour
{
 
    public GameObject itemBox;
    public int numberOfBoxes;

    public int modifyXPosition;
    public int modifyZPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnBoxes();
    }

    private void Update()
    {
        
    }
    private void spawnBoxes()
    {
        for (int i = 0; i < numberOfBoxes; i++)
        {
            Vector3 name = new Vector3(transform.position.x + modifyXPosition * i, transform.position.y, transform.position.z + modifyZPosition * i);
            GameObject itemBoxClone = Instantiate(itemBox, name, transform.rotation);

           
        }
    }
}
