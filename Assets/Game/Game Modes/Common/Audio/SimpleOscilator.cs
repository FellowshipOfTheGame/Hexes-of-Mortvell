using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleOscilator : MonoBehaviour
{
	public float frequency;
	public int maxState;
	public float perc;
	
	private float constant;
	private int state;
    // Start is called before the first frame update
    void Awake()
    {
        constant = 40 * Mathf.PI * frequency / 1000;
    }

    // Update is called once per frame
    void OnAudioFilterRead(float[] data, int channels)
    {
        float norm;
		if(state > 0){
			for(int i=0; i<data.Length/channels; i++){
				norm = i;
				norm /= data.Length/channels;
				for(int j = 0; j<channels; j++){
					data[channels*i + j] = Mathf.Cos(norm * constant)*perc + data[channels*i + j]*(1-perc);
				}
			}
			for(int i=10; i<data.Length; i++){
				data[i] = 0.9f*data[i] + 0.1f*data[i-10];
			}
			state--;
		}
    }
	
	public void play(){
		state = maxState;
	}
}
