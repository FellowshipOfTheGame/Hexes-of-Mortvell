using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveVolumeScript : MonoBehaviour
{
    public void SaveVolumes(){
		PlayerPrefs.Save();
	}
}
