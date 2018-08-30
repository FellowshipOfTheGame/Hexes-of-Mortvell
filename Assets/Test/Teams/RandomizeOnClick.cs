using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeOnClick : MonoBehaviour
{
	public TeamColorRandomizer randomizer;
	public int mouseButton;

	void Update()
	{
		if (Input.GetMouseButtonDown(mouseButton))
			randomizer.Randomize();
	}
}
