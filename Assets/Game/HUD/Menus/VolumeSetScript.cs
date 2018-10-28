using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetScript : MonoBehaviour
{
	public string Param;

	private Slider Sl;
    // Start is called before the first frame update
    void Start()
    {
        Sl = GetComponent<Slider>();
		Sl.value = PlayerPrefs.GetFloat(Param);
    }

    void onValueChange()
    {
        PlayerPrefs.SetFloat(Param,Sl.value);
    }
}
