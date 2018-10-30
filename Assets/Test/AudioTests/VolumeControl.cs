using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour
{
    private float vol;


    void Update()
    {
        vol = PlayerPrefs.GetFloat("SFX");
    }

    // Update is called once per frame
    void OnAudioFilterRead(float[] Data, int channel)
    {
        for(int i=0;i<Data.Length; i++)
        {
            Data[i] *= vol;
        }
    }
}
