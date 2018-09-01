using UnityEngine;
using HexCasters.DesignPatterns.Fsm;

namespace HexCasters.Testing.GameModeTest
{
	public class GameModeTestTurnEndState : FsmState
	{
		public GameModeTestTurn turn;

		public override void Enter()
		{
			this.fsm.Transition<GameModeTestTurnStartState>();
		}

		public override void Exit()
		{
			this.turn.NextTeam();
		}
	}
}
