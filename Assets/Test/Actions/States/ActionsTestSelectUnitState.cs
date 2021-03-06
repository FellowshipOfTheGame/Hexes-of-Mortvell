﻿using System;
using System.Linq;
using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Units.Teams;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Hud.Grid;

namespace HexesOfMortvell.Testing.ActionsTest
{
	public class ActionsTestSelectUnitState : FsmState
	{
		public ActionsTestTurn turn;
		public ActionsTestCellClickListener clickListener;
		public ActionsTestPlayerOrder playerOrder;

		private IDisposable unmovedUnitsHighlight;

		public override void Enter()
		{
			this.playerOrder.Clear();
			ApplyHighlightToUnmovedUnits();

			this.clickListener.cellClickedEvent += CellClicked;
		}

		public override void Exit()
		{
			this.clickListener.cellClickedEvent -= CellClicked;
			this.unmovedUnitsHighlight.Dispose();
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
				this.fsm.Transition<ActionsTestTurnEndState>();
		}

		void ApplyHighlightToUnmovedUnits()
		{
			var units = this.turn.CurrentTeam.Members
				.Select(
					teamMember => teamMember.GetComponent<BoardCellContent>());
			var unitCells = units
				.Where(unit => !unit.GetComponent<ActionsTestUnit>().hasMoved)
				.Select(
					cellContent => cellContent.Cell);
			this.unmovedUnitsHighlight = unitCells.AddHighlightLayer(Color.white);
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
