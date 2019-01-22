using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPrefabScript : MonoBehaviour
{
	public RockColision colSound;
	public float maxTime;
	
	private float timeLeft;
	
    // Start is called before the first frame update
    void Start()
    {
        var windSound = GetComponent<TimedWhiteNoise>();
		windSound.startSound();
		timeLeft = maxTime;
    }

	void Update(){
		maxTime -= Time.deltaTime;
		if(maxTime < 0){
			float time = 0.020f;
			time *= colSound.duration;
			colide();
			Destroy(gameObject,time);
		}
	}
	
	public void colide(){
		colSound.play();
		//StartCoroutine(DestroyAfterPlaying());
	}
	
	IEnumerator DestroyAfterPlaying() {
		while (!colSound.donePlaying)
			yield return null;
		Destroy(this.gameObject);
	}
}
