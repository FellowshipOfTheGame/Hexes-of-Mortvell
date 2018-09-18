using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.Testing.ActionsTest
{
	public class ActionsTestTurnStartState : FsmState
	{
		public ActionsTestTurn turn;

		public override void Enter()
		{
			ResetMovementPoints();
			this.fsm.Transition<ActionsTestSelectUnitState>();
		}

		public override void Exit() {}

		void ResetMovementPoints()
		{
			foreach (var teamMember in turn.CurrentTeam.Members)
			{
				var unit = teamMember.GetComponent<ActionsTestUnit>();
				unit.hasMoved = false;
			}
		}
	}
}
