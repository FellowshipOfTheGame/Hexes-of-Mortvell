using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverb : MonoBehaviour
{

	public int delay;
	public float amplitude;
	
	private float[] last;
    void Awake()
    {
        last = new float[delay];
    }

    
    void OnAudioFilterRead(float[] data, int channels)
    {
        for(int i=0;i<delay;i++){
			data[i] = (1-amplitude)*data[i] + amplitude*last[i];
		}
		for(int i=delay; i<data.Length; i++){
			data[i] = (1-amplitude)*data[i] + amplitude*data[i - delay];
		}
		for(int i = 0; i<delay; i++){
			last[i] = data[data.Length - delay - 1 + i];
		}
    }
}
