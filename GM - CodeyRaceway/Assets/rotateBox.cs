using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateBox : MonoBehaviour
{
    public float spinSpeed = 10f;
    private float timeElapsed = 0f;
    public float ybuffer = 10f;

    Material mat;
    Color boxShine;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().materials[0];
        mat.color = Color.black;
        boxShine = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, spinSpeed, 0f));
        timeElapsed += Time.deltaTime;
        float amp = Mathf.Sin(timeElapsed);
        transform.position = new Vector3(transform.position.x, amp + ybuffer, transform.position.z);

        TestColor();
    }

    void TestColor()
    {
        float value = Mathf.Sin(timeElapsed - (Mathf.PI * 0.5f));
        value += 1f;
        value *= 0.5f;

        mat.color = boxShine * value; ;
    }
}
