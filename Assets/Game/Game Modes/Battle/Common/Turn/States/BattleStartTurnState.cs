using System.Linq;
using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.GameModes.Battle.Common
{
	public class BattleStartTurnState : FsmState
	{
		public BattleTurn turn;

		public override void Enter()
		{
			Debug.Log(GetType());
			ResetCurrentTeamUnitMovement();
			this.fsm.Transition<BattleOverviewState>();
		}

		public override void Exit() {}

		void ResetCurrentTeamUnitMovement()
		{
			var currentTeamUnits = this.turn.CurrentTeam.Members
				.Select(member => member.GetComponent<Unit>())
				.Where(unit => unit != null);
			foreach (var unit in currentTeamUnits)
				unit.hasMoved = false;
		}
	}
}