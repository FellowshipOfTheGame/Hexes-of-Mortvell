using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderScript : MonoBehaviour
{
	public int maxState;
	
	private int state;
	
	public void play(){
		state = maxState;
	}
	
	void OnAudioFilterRead(float[] data, int channels){
		float sawtooth = 2*channels;
		float env;
		int x = (maxState - state);
		sawtooth /= data.Length;
		if(state > 5){
			for(int i = 0; i<data.Length/channels; i++){
				env = i;
				env /= data.Length/channels;
				env = 1 - (env)*(env);
				//Debug.Log(env);
				for(int j=0; j<channels; j++)
					data[channels * i + j] = sawtooth * i * env;
			}
			state --;
		}else if (state > 0){
			for(int i = 0; i<data.Length/channels; i++){
				env = i;
				//env /=data.Length/channels;
				//env = state % 2 - env * env * env * env;
				env = state % 2 - i*i*i*i;
				for(int j=0; j<channels; j++)
					data[channels*i + j] = env * state/(maxState-5);
			}
			state --;
		}
	}
}
