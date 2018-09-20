using System.Linq;
using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.GameModes.Battle.Common
{
	public class BattleStartTurnState : FsmState
	{
		public Board board;
		public BattleTurn turn;

		public override void Enter()
		{
			Debug.Log(GetType());
			ResetCurrentTeamUnitMovement();
			// TODO turn start event (maybe?)
			CommitHPValues();
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

		void CommitHPValues()
		{
			for (var x = this.board.MinX; x <= this.board.MaxX; x++)
				for (var y = this.board.MinY; y <= this.board.MaxY; y++)
					this.board[x, y].Content?.GetComponent<HP>()?.Commit();
		}
	}
}