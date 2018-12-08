using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedWhiteNoise : MonoBehaviour
{
    public float duration;
    public float upDuration;
    public float downDuration;
    public float HighFilterBase;
    public float HighFilterRange;
    public float LowFilterFrequency;
    public AudioHighPassFilter HighFilt;
    public AudioLowPassFilter LowFilt;

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
        LowFilt.cutoffFrequency = LowFilterFrequency;
    }

    private void Update()
    {
        if (upTimeLeft > 0)
        {
            amp = 1 - upTimeLeft / upDuration;
            upTimeLeft -= Time.deltaTime;
            HighFilt.cutoffFrequency = HighFilterBase - HighFilterRange * amp;
        }
        else if (timeLeft > 0 && timeLeft <= downTimeLeft){
            amp = downTimeLeft / downDuration;
            HighFilt.cutoffFrequency = HighFilterBase - HighFilterRange * amp;
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
                data[i] = (float)(rand.NextDouble() * 2 - 1)*amp;
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
		Debug.Log("foi");
    }
}
