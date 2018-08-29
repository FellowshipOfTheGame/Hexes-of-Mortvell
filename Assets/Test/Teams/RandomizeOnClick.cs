using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeOnClick : MonoBehaviour
{
	public TeamColorRandomizer randomizer;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
			randomizer.Randomize();
	}
}
