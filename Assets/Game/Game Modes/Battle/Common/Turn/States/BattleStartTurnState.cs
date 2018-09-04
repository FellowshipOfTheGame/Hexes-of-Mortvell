using UnityEngine;
using HexCasters.Core.Grid;
using HexCasters.DesignPatterns.Fsm;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattleStartTurnState : FsmState
	{
		public BattleTurn turn;

		public override void Enter()
		{
			ResetCurrentTeamMovables();
			this.fsm.Transition<BattleOverviewState>();
		}

		public override void Exit() {}

		void ResetCurrentTeamMovables()
		{
			var currentTeamMembers = turn.CurrentTeam.Members;
			foreach (var member in currentTeamMembers)
			{
				var movable = member.GetComponent<Movable>();
				if (movable == null)
					continue;
				movable.hasMoved = false;
			}
		}
	}
}