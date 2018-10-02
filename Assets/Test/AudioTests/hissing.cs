using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hissing : MonoBehaviour
{
    public int chance;

    private bool sounding;
    private int amp;
    System.Random rand;

    private void Start()
    {
        sounding = false;
        amp = 0;
        rand = new System.Random();
    }

    void OnAudioFilterRead(float[] data,int channel)
    {
        float r;
        int dur = (int)(rand.NextDouble() * 100);
        for(int i = 0; i < data.Length ; i++)
        {
            r = (float)(rand.NextDouble() * 2 - 1) * amp;
            if(dur > chance)
                data[i] = (float) (0.95 * data[i] + 0.05 * Mathf.Pow(r, 5));
        }
    }

    public void toggleSound()
    {
        sounding = !sounding;
        if (sounding) amp = 1;
        else amp = 0;
    }
}
