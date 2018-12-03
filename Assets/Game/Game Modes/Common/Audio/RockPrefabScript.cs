using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPrefabScript : MonoBehaviour
{
	public RockColision colSound;
	
    // Start is called before the first frame update
    void Start()
    {
        var windSound = GetComponent<TimedWhiteNoise>();
		windSound.startSound();
    }

	public void colide(){
		colSound.play();
		StartCoroutine(DestroyAfterPlaying());
	}
	
	IEnumerator DestroyAfterPlaying() {
		while (!colSound.donePlaying)
			yield return null;
		Destroy(this.gameObject);
	}
}
