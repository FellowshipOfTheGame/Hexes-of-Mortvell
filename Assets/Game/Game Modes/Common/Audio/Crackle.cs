using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crackle : MonoBehaviour
{
    public int chance;
    public double amp;

    private bool sounding;
    System.Random rand;

    private void Start()
    {
        sounding = false;
        rand = new System.Random();
    }

    void OnAudioFilterRead(float[] data,int channel)
    {
        float r;
        int dur = (int)(rand.NextDouble() * 100);
        for(int i = 0; i < data.Length && sounding ; i++)
        {
            r = (float)(rand.NextDouble() * 2 - 1);
            if (dur > chance) 
                data[i] = (float) ((1-amp) * data[i] + amp * Mathf.Pow(r, 5));
        }
    }

    public void toggleSound()
    {
        sounding = !sounding;
    }
}
