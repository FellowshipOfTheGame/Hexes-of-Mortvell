using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPrefabScript : MonoBehaviour
{
	public float lifeLength;
    // Start is called before the first frame update
    void Start()
    {
		Destroy(gameObject, lifeLength);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
