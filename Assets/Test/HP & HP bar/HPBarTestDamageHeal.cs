using UnityEngine;
using HexCasters.Core.Units;

public class HPBarTestDamageHeal : MonoBehaviour
{
	public HP hp;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			hp.Decrease(1, commit: true);
		if (Input.GetKeyDown(KeyCode.RightArrow))
			hp.Increase(1, commit: true);
		if (Input.GetKeyDown(KeyCode.X))
			DoWeirdStuff();
	}

	void DoWeirdStuff()
	{
		// Effectively reduces HP by 1, but goes way below 0
		// Should still only trigger death checker when the final value
		// (after commit) is 0.
		hp.Decrease(3);
		hp.Increase(2);
		hp.Decrease(5);
		hp.Increase(6);
		hp.Decrease(20);
		hp.Increase(19);
		hp.Commit();
	}
}
