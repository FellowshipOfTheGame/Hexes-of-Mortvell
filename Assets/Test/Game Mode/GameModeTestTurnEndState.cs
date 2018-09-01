using UnityEngine;
using HexCasters.DesignPatterns.Fsm;

namespace HexCasters.Testing.GameModeTest
{
	public class GameModeTestTurnEndState
		: FsmState<GameModeTestTurnFsmSharedMemory>
	{
		public override void Enter()
		{
			this.fsm.Transition<GameModeTestTurnStartState>();
		}

		public override void Exit() {
			this.sharedMemory.turn.NextTeam();
		}
	}
}
