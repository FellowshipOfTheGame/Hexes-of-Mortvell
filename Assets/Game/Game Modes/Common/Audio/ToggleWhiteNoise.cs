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
	public float probMov;
	public float variation;
	public float range;
	
    private bool sounding;
    private float upTimeLeft;
    private float downTimeLeft;
    private float amp;
    private AudioHighPassFilter HighFilter;
    private AudioLowPassFilter LowFilter;
	private HexesOfMortvell.Core.Grid.WeatheredCellsCounter counter;
    System.Random rand;

    private void Awake()
    {
        rand = new System.Random();
		HighFilter = GetComponent<AudioHighPassFilter>();
		LowFilter = GetComponent<AudioLowPassFilter>();
        LowFilter.cutoffFrequency = LowPassFrequency;
		counter = GetComponent<HexesOfMortvell.Core.Grid.WeatheredCellsCounter>();
		sounding = false;
		toggleSound();
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
			float weather = counter.GetMostCommon();
			amp = weather;
			Debug.Log(name + amp.ToString());
			double val = rand.NextDouble(),probUp,probDn;
			if(val<probMov){
				val = rand.NextDouble();
				probDn = (HighFilter.cutoffFrequency - HighPassBase)/range;
				probUp = 1 - probDn;
				if(val<probUp){
					HighFilter.cutoffFrequency += variation;
				}else {	
					HighFilter.cutoffFrequency -= variation;
				}
			}
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
