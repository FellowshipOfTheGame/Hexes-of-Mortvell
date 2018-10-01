using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWhiteNoise : MonoBehaviour
{
    public float upDuration;
    public float downDuration;
    public float HighPassBase;
    public float HighPassRange;
    public float LowPassFrequency;
    public AudioHighPassFilter HighFilter;
    public AudioLowPassFilter LowFilter;

    private bool sounding;
    private float upTimeLeft;
    private float downTimeLeft;
    private float amp;
    System.Random rand;

    private void Start()
    {
        rand = new System.Random();
        sounding = false;
        LowFilter.cutoffFrequency = LowPassFrequency;
    }

    private void Update()
    {
        if (upTimeLeft > 0)
        {
            amp = 1 - upTimeLeft / upDuration;
            upTimeLeft -= Time.deltaTime;
            HighFilter.cutoffFrequency = HighPassBase - HighPassRange * amp;
        }
        else if (downTimeLeft > 0){
            amp = downTimeLeft / downDuration;
            HighFilter.cutoffFrequency = HighPassBase - HighPassRange * amp;
            downTimeLeft -= Time.deltaTime;
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = (float)(rand.NextDouble() * 2 - 1) * amp;
        }
    }
    public void toggleSound()
    {
        if(sounding)
            downTimeLeft = downDuration;
        else
            upTimeLeft = upDuration;

        sounding = !sounding;
    }
}
