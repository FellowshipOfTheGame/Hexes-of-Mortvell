using UnityEngine;
using HexCasters.DesignPatterns.Fsm;

namespace HexCasters.Testing.ActionsTest
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
				var movable = teamMember.GetComponent<ActionsTestMovable>();
				movable.hasMoved = false;
			}
		}
	}
}
