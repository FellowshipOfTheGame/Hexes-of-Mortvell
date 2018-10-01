using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteNoise : MonoBehaviour
{
    public float duration;
    public float upDuration;
    public float downDuration;
    public AudioHighPassFilter filt;

    private float ticksLeft;
    private float upTimeLeft;
    private float timeLeft;
    private float downTimeLeft;
    private float amp;
    private int sampleRate;
    System.Random rand;

    private void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        rand = new System.Random();
    }

    private void Update()
    {
        if (upTimeLeft > 0)
        {
            amp = upTimeLeft / upDuration;
            upTimeLeft -= Time.deltaTime;
            filt.cutoffFrequency = 600 + 100 * amp;
        }
        else if (timeLeft > 0 && timeLeft <= downTimeLeft){
            amp = downTimeLeft / downDuration;
            filt.cutoffFrequency = 700 - 100 * amp;
            downTimeLeft -= Time.deltaTime;
        }
        timeLeft-= Time.deltaTime;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (ticksLeft > 0)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (float)(rand.NextDouble() * 2 - 1);
            }
            ticksLeft -= data.Length / channels;
        }
    }
    public void startSound()
    {
        ticksLeft = duration * sampleRate;
        timeLeft = duration;
        upTimeLeft = upDuration;
        downTimeLeft = downDuration;
    }
}
