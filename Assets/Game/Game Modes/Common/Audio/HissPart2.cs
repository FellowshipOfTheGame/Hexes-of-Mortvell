using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HissPart2 : MonoBehaviour
{
    public int con;
    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] *= 10;
            data[i] *= data[i] * data[i] * con;
        }
    }
}
