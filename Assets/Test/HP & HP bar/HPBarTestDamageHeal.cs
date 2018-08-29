using UnityEngine;
using HexCasters.Core.Units;

public class HPBarTestDamageHeal : MonoBehaviour
{
	public HP hp;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			hp.Decrease(1, true);
		if (Input.GetKeyDown(KeyCode.RightArrow))
			hp.Increase(1, true);
	}
}
