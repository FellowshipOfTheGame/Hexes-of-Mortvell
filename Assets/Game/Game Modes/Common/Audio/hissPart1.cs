using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hissPart1 : MonoBehaviour
{
    public float LowFreq;
    public AudioLowPassFilter filt;

    private bool sounding;
    private int amp;
    System.Random rand;
    // Start is called before the first frame update
    void Start()
    {
        filt.cutoffFrequency = LowFreq;
        sounding = false;
        amp = 0;
        rand = new System.Random();
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for(int i = 0; i<data.Length; i++)
        {
            data[i] = (float) (rand.NextDouble()*2 - 1)*amp;
        }
    }

    public void toggleSound()
    {
        sounding = !sounding;
        if (sounding) amp = 1;
        else amp = 0;
    }
}
