using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.Testing.ActionsTest
{
	public class ActionsTestTurnEndState : FsmState
	{
		public ActionsTestTurn turn;
		public override void Enter()
		{
			this.turn.NextTeam();
			this.fsm.Transition<ActionsTestTurnStartState>();
		}

		public override void Exit() {}
	}
}
