using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamsTestRandomizeOnClick : MonoBehaviour
{
	public TeamsTestTeamColorRandomizer randomizer;
	public int mouseButton;

	void Update()
	{
		if (Input.GetMouseButtonDown(mouseButton))
			randomizer.Randomize();
	}
}
