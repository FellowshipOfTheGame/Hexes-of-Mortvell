using System.Linq;
using System.Collections;
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
			CommitHPValues();
			ResetCurrentTeamUnitMovement();
			// TODO turn start event (maybe?)
			StartCoroutine(TransitionToOverviewOnNextFrame());
		}

		public override void Exit() {}

		// Needed to allow the death event of units to propagate
		IEnumerator TransitionToOverviewOnNextFrame()
		{
			yield return new WaitForEndOfFrame();
			this.fsm.Transition<BattleOverviewState>();
		}

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