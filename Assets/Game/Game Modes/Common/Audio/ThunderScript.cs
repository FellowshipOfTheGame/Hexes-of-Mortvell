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
				env = 1 - (x+i)*(x+i)/10000.0f;
				for(int j=0; j<channels; j++)
					data[channels * i + j] = sawtooth * i * env;
			}
			state --;
		}else if(state > 3){
			for(int i = 0; i<data.Length/channels; i++){
				env = i;
				//env /=data.Length/channels;
				env = 1 - env * env * env * env;
				for(int j=0; j<channels; j++)
					data[channels*i + j] = env;
			}
			state --;
		}else if(state > 0){
			for(int i=0; i<data.Length/channels; i++){
				//env = i;
				//env /= data.Length/channels;
				sawtooth = -i*i*i*i;
				env = i / (float)data.Length;
				for(int j=0;j<channels;j++)
					data[channels*i + j] = env * sawtooth;
			}
			state --;
		}
	}
}
