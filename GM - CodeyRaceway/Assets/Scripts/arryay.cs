using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arryay : MonoBehaviour
{
    public GameObject Barthing;
    public float spacing;
    public int SampleSize = 256;
    public float multiplier = 1f;

    private GameObject[] bob;
    private AudioSource audio;
    private float[] samples;
    private int amount;


    // Start is called before the first frame update
    void Start()
    {
        amount = SampleSize;

        bob = new GameObject[amount];
        audio = GetComponent<AudioSource>();
        samples = new float[SampleSize];

        createBar(amount, spacing);
    }

    // Update is called once per frame
    void Update()
    {
        Test();
    }

    public void createBar(int amount, float spacing)
    {
        for (int i = 0; i < amount; i++)
        {
            float total = bob.Length + (spacing * ((float)amount - 1));
            total = total / 2f;
           
            Vector3 barPos = new Vector3(spacing * i, 0, 0);
            barPos.x = barPos.x - total; 
            
            bob[i] = Instantiate(Barthing, barPos , transform.rotation);

            //bob[i].transform.localScale = transform.localScale * i; 
        }
    }

    void Test()
    {
        audio.GetSpectrumData(samples, 0, FFTWindow.Blackman);

        for(int i = 0; i < samples.Length; i++)
        {
            Vector3 newScale = Vector3.one;
            newScale.y *= (samples[i] * multiplier);

            bob[i].transform.localScale = newScale;
        }

    }
}
