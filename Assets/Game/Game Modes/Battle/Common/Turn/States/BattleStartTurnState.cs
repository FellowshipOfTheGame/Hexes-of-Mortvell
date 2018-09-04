using System.Linq;
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
			Debug.Log(GetType());
			ResetCurrentTeamMovables();
			this.fsm.Transition<BattleOverviewState>();
		}

		public override void Exit() {}

		void ResetCurrentTeamMovables()
		{
			var currentTeamMovables = this.turn.CurrentTeam.Members
				.Select(member => member.GetComponent<Movable>())
				.Where(movable => movable != null);
			foreach (var movable in currentTeamMovables)
				movable.hasMoved = false;
		}
	}
}