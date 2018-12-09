using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleOscilator : MonoBehaviour
{
	public float frequency;
	public int maxState;
	public float perc;
	
	private float constant;
	private float beating;
	private int state;
	private System.Random rand;
    // Start is called before the first frame update
    void Awake()
    {
        constant = 40 * Mathf.PI * frequency / 1000;
		beating = 39 * Mathf.PI * frequency / 1000;
		rand = new System.Random();
    }

    // Update is called once per frame
    void OnAudioFilterRead(float[] data, int channels)
    {
        float norm;
		if(state > 1){
			for(int i=0; i<data.Length/channels; i++){
				norm = i;
				norm /= data.Length/channels;
				for(int j = 0; j<channels; j++){
					data[channels*i + j] = Mathf.Cos(norm * constant)*perc/2 + Mathf.Cos(norm* beating)*perc/2 + data[channels*i + j]*(1-perc);
				}
			}
			for(int i=20; i<data.Length; i++){
				data[i] = 0.75f*data[i] + 0.125f*data[i-10] + 0.125f*data[i-20];
			}
			state--;
		}else if(state > maxState / 10){
			for(int i = 0; i<data.Length/channels; i++){
				norm = i;
				norm /= data.Length/channels;
				for(int j=0; j<channels; j++){
					data[channels*i + j] = Mathf.Cos(norm * constant) * perc/2 * (1-norm) + Mathf.Cos(norm * beating) * perc/2 * (1-norm) + data[channels*i + j]*(1-perc);
				}
			}
			state --;
		}
    }
	
	public void play(){
		state = maxState;
	}
}
