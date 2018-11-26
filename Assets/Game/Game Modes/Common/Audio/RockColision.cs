using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockColision : MonoBehaviour
{
	public int duration;
	
	public int state { get; private set; }
	
	public bool donePlaying {
		get { return this.state == 0; }
	}
	private int amp;
	
	public RockColision()
	{
		this.state = -1;
	}
	
    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update(){}
	
	void OnAudioFilterRead(float[] data, int channels){
		int dataLen = data.Length / channels;
		int SampleNeeded,position;
		float toAdd,amp,SamplePerSecond;
		SamplePerSecond = dataLen/0.02f;
		SampleNeeded = (int)(SamplePerSecond / 10);
		//Debug.Log(SamplePerSecond);
		if(state > 0){
			for(int i=0;i<dataLen;i+=channels){
				position = dataLen*(duration-state) + i;
				amp = 1 - (position)/(duration*dataLen);
				
				//toAdd=((position % (SampleNeeded)) - SampleNeeded/2)/(SampleNeeded/2);
				
				toAdd = Mathf.Sin(position/10000f);
				
				//Debug.Log(toAdd);
				
				//toAdd*=5;
				//toAdd*= (amp*amp*amp*amp);
				for(int j=0;j<channels;j++) data[channels*i+j] += toAdd* amp* 100;
			}
			state --;
		}
	}
	
	public void play(){
		state = duration;
	}
}
