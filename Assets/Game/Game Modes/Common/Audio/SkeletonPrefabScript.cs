using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPrefabScript : MonoBehaviour
{
	public Crackle timedCrackle;
	public float waitTime;
	public float lifeLength;
	
	private float waitLeft;
    // Start is called before the first frame update
    void Start()
    {
		var windSound = GetComponent<TimedWhiteNoise>();
		windSound.startSound();
        waitLeft = waitTime;
		Destroy(gameObject, lifeLength);
    }

    // Update is called once per frame
    void Update()
    {
        waitLeft -= Time.deltaTime;
		if(waitLeft > 0){
			timedCrackle.enabled = true;
		}
    }
}
