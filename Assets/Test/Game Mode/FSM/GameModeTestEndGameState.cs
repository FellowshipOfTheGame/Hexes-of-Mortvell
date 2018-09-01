using UnityEngine;
using HexCasters.DesignPatterns.Fsm;

namespace HexCasters.Testing.GameModeTest
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