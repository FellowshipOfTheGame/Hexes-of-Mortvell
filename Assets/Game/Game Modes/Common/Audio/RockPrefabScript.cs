using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPrefabScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var windSound = GetComponent<TimedWhiteNoise>();
		windSound.startSound();
    }

	public void colide(){
		var colSound = GetComponent<RockColision>();
		colSound.play();
		Destroy(this.gameObject, 0.02f);
	}
}
