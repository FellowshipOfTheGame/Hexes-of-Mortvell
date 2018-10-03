using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hissingComplete : MonoBehaviour
{
    public int multiplier;

    private bool sounding;
    private int amp;
    System.Random rand;
    // Start is called before the first frame update
    void Start()
    {
        sounding = false;
        amp = 0;
        rand = new System.Random();
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        float[] temp = new float[data.Length];
        float f = (float)rand.NextDouble(), phi = (float)(rand.NextDouble() * 2 * Mathf.PI - Mathf.PI);
        temp[0] = (float)(rand.NextDouble() * 2 - 1) * amp;
        temp[1] = (float)(rand.NextDouble() * 2 - 1) * amp - 0.5f * temp[0];
        temp[2] = (float)(rand.NextDouble() * 2 - 1) * amp - 0.5f * temp[1] - 0.25f * temp[2];
        for (int i = 3; i < data.Length; i++)
        {
            temp[i] = (float)(rand.NextDouble() * 2 - 1) * amp;
            temp[i] -= 0.5f * temp[i - 1] + 0.25f * temp[i-2] + 0.125f * temp[i-3];

        }
        for(int i = 0; i < data.Length; i++)
        {
            temp[i] /= 10;
            temp[i] *= temp[i] * temp[i] * multiplier * (float)Mathf.Sin(f*i/(float)Mathf.PI+phi);
            data[i] = (float)(0.995 * data[i] + 0.005 * temp[i]);
        }
    }

    public void toggleSound()
    {
        sounding = !sounding;
        if (sounding) amp = 1;
        else amp = 0;
    }
}
