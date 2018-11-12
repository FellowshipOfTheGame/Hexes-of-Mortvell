using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolume : MonoBehaviour
{
	private AudioSource AS;
    // Start is called before the first frame update
	void Awake(){
		//set the first value for music volume, if none was set before
		var tmp = PlayerPrefs.GetFloat("BGM",-1f);
		if(tmp == -1f)
			PlayerPrefs.SetFloat("BGM",1f);
		//set the first value for SFX Volume, if none was set before
		tmp = PlayerPrefs.GetFloat("SFX", -1f);
		if(tmp == -1f)
			PlayerPrefs.SetFloat("SFX",1f);
	}
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		var vol = PlayerPrefs.GetFloat("BGM");
        AS.volume = vol;
    }
}
