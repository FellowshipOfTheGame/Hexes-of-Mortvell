using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.Testing.GameModeTest
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
