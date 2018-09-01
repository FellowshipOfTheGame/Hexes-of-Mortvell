using UnityEngine;
using HexCasters.DesignPatterns.Fsm;

namespace HexCasters.Testing.GameModeTest
{
	public class GameModeTestTurnStartState : FsmState
	{
		public override void Enter()
		{
			this.fsm.Transition<GameModeTestSelectDudeState>();
		}

		public override void Exit() {}
	}
}
