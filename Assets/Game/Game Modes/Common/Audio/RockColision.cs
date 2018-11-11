using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockColision : MonoBehaviour
{
	public int duration;
	
	private int state;
	private int amp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
	
	void OnAudioFilterRead(float[] data, int channels){
		int dataLen = data.Length / channels;
		int SampleNeeded,position;
		float toAdd,amp;
		if(state > 0){
			for(int i=0;i<dataLen;i+=channels){
				position = dataLen*(duration-state);
				amp = 1 - (position + i)/(duration*dataLen);
				
				SampleNeeded = (int)(/*freq*/ 1000.0/20 * dataLen/20 /*length till next call*/);
				toAdd=1 - (position % SampleNeeded - SampleNeeded/2)/SampleNeeded;
			
				SampleNeeded = (int)(/*freq*/ 1000.0/40 * dataLen/ /*length till next call*/20);
				toAdd=1 - (position % SampleNeeded - SampleNeeded/2)/(float)(2*SampleNeeded);
				
				SampleNeeded = (int)(/*freq*/ 1000.0/60 * dataLen/ /*length till next call*/20);
				toAdd=1 - (position % (SampleNeeded - SampleNeeded/2))/(3*SampleNeeded);
				
				SampleNeeded = (int)(/*freq*/ 1000.0/80 * dataLen/ /*length till next call*/20);
				toAdd+=1 - (position % SampleNeeded - SampleNeeded/2)/(4*SampleNeeded);
				
				toAdd*=5;
				//toAdd*= (amp*amp*amp*amp);
				for(int j=0;j<channels;j++) data[channels*i+j] += toAdd;
			}
			state --;
		}
	}
	
	public void play(){
		state = duration;
	}
}
