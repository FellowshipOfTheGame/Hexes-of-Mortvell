using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWhiteNoise : MonoBehaviour
{
	public int SoundType;//type 0: fire; 1: rain; 2: Snow
    public float upDuration;
    public float downDuration;
    public float HighPassBase;
    public float HighPassRange;
    public float LowPassFrequency;
    public AudioHighPassFilter HighFilter;
    public AudioLowPassFilter LowFilter;
	public float probMov;
	public float variation;
	public float range;

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
		//counter = GameObject.FindObjectOfType<WeatheredCellsCounter>();
    }

    private void Update()
    {
        if (upTimeLeft > 0)
        {
            amp = 1 - upTimeLeft / upDuration;
            upTimeLeft -= Time.deltaTime;
            HighFilter.cutoffFrequency = HighPassBase - HighPassRange * amp;
        }
        else if (downTimeLeft > 0)
        {
            amp = downTimeLeft / downDuration;
            HighFilter.cutoffFrequency = HighPassBase - HighPassRange * amp;
            downTimeLeft -= Time.deltaTime;
        }
        else if(!sounding) amp = 0;
		else{
			double val = rand.NextDouble(),probUp,probDn;
			if(val<probMov){
				val = rand.NextDouble();
				probDn = (HighFilter.cutoffFrequency - HighPassBase)/range;
				probUp = 1 - probDn;
				if(val<probUp){
					Debug.Log("up");
					HighFilter.cutoffFrequency += variation;
				}else {	
					Debug.Log("down");
					HighFilter.cutoffFrequency -= variation;
				}
			}
		}
		/*
		float weather = counter.GetMostCommon();
		if(((int)weather) == type){
			amp *= (weather - (int)weather);
		}
		*/
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
