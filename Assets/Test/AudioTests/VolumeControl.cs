using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour
{
    private AudioSource AS;
    private float vol;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        vol = AS.volume;
    }

    void Update()
    {
        vol = AS.volume;
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
