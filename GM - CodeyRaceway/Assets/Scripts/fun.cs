using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fun : MonoBehaviour
{
    GameObject[] bob;

    public GameObject prefab;
    public int column;
    public int row;

    // Start is called before the first frame update
    void Start()
    {
        
        bob = new GameObject[column * row];
        fill();
        list();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fill ()
    {
        int offsetX = 0;
        int offsetY = 0;
        int number = 0;
       
        for(int i = 0; i < column; i++)
        {
            for (int a = 0; a < row; a++)
            {
                Vector3 orange = transform.position + new Vector3(offsetX, offsetY, 0);
                offsetX += 10;
                bob[number] = Instantiate(prefab, orange, transform.rotation);
                number++;
            }
            offsetY += 10;
            offsetX = 0;
        }
        Debug.Log(bob);
    }


    void list()
    {
        for (int a = 0; a < bob.Length; a++)
        {
            float val = a / (bob.Length - 1);
            bob[a].GetComponent<MeshRenderer>().material.color = new Color(val, val, val);

            Debug.Log(val);
        }
        
        //foreach(GameObject x in bob)
        //{
           
       // }

    }

}
