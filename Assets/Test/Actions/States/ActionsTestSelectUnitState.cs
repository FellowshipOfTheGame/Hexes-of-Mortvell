using System;
using System.Linq;
using UnityEngine;
using HexCasters.Core.Grid;
using HexCasters.Core.Units.Teams;
using HexCasters.DesignPatterns.Fsm;
using HexCasters.Hud.Grid;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestSelectUnitState : FsmState
	{
		public ActionsTestTurn turn;
		public ActionsTestCellClickListener clickListener;
		public ActionsTestPlayerOrder playerOrder;

		private IDisposable unmovedUnitsHighlight;

		public override void Enter()
		{
			ResetPlayerOrder();
			ApplyHighlightToUnmovedUnits();

			this.clickListener.cellClickedEvent += CellClicked;
		}

		public override void Exit()
		{
			this.clickListener.cellClickedEvent -= CellClicked;
			this.unmovedUnitsHighlight.Dispose();
		}

		void ResetPlayerOrder()
		{
			this.playerOrder.selectedUnit = null;
			this.playerOrder.moveDest = null;
		}

		void ApplyHighlightToUnmovedUnits()
		{
			var units = this.turn.CurrentTeam.Members
				.Select(
					teamMember => teamMember.GetComponent<BoardCellContent>());
			var unitCells = units
				.Where(unit => !unit.GetComponent<ActionsTestMovable>().hasMoved)
				.Select(
					cellContent => cellContent.Cell);
			this.unmovedUnitsHighlight = unitCells.Highlight(Color.white);
		}

		void CellClicked(BoardCell cell)
		{
			if (cell.Empty)
				return;
			var content = cell.Content;
			var teamMember = content.GetComponent<TeamMember>();
			if (teamMember == null || teamMember.team != turn.CurrentTeam)
				return;
			SelectUnit(cell.Content);
		}

		void SelectUnit(BoardCellContent unit)
		{
			this.playerOrder.selectedUnit = unit;
			this.fsm.Transition<ActionsTestSelectMoveDestState>();
		}
	}
}
