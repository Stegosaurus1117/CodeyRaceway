using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int name = whatever(2, 4);
        Debug.Log(name);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int whatever(int bob, int Charles)
    {
        int answer = bob;

        for (; Charles > 1; Charles--)
            {
                answer *= bob;
            }
        return answer;
    }

    
}
