using UnityEngine;

namespace HexesOfMortvell.Testing.GameModeTest
{
	public class GameModeTestReferee : MonoBehaviour
	{
		public GameModeTestTurn turn;
		public GameObject thingy;

		public void AwardVictoryToCurrentTeam()
		{
			thingy.SetActive(true);
			Debug.Log(turn.CurrentTeam.name);
		}
	}
}