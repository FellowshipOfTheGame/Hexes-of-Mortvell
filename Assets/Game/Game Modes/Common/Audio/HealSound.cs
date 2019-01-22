using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSound : MonoBehaviour
{
	public float minFreq;
	public float maxFreq;
	public int[] stateLen;
	
	private float freq;
	private float constant;
	private int currState;
	private int itLeft; //iterations left in the current state;
    // Start is called before the first frame update
    void Awake()
    {
        currState = stateLen.Length;
		constant = 40 * Mathf.PI/1000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnAudioFilterRead(float[] data, int channels){
		int i,j;
		float x;
		float smallVal = 0f;
		float bigVal = 0f;
		if(currState < stateLen.Length){
			if(currState == 0){
				smallVal = constant * minFreq; //root
				bigVal = constant* minFreq * 1.331f; //minor third
			}else if(currState == 1){
				smallVal = constant * minFreq;
				bigVal = constant * minFreq * 1.5f; //perfect fifth
			}else if(currState == 2){
				smallVal = constant * maxFreq;
				bigVal = constant * maxFreq * 1.4641f; //major third
			}
			for(i = 0;i < data.Length/channels; i++){
				x = i;
				x /= data.Length/channels;
				for(j = 0; j<channels/2;j++){
					data[channels*i + j] = Mathf.Cos(smallVal * x);
				}
				for(j = channels/2; j<channels; j++){
					data[channels*i + j] = Mathf.Cos(bigVal * x);
				}
			}
			itLeft --;
			if(itLeft <= 0){
				currState ++;
				if(currState < stateLen.Length)
					itLeft = stateLen[currState];
			}
		}
	}
	
	public void play(){
		currState = 0;
		itLeft = stateLen[0];
	}
}
