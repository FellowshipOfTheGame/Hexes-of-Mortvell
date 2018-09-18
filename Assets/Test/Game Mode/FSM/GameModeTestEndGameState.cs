using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.Testing.GameModeTest
{
	public class GameModeTestEndGameState : FsmState
	{
		public GameObject thingy;

		public override void Enter()
		{
			thingy?.SetActive(true);
		}

		public override void Exit() {}
	}
}