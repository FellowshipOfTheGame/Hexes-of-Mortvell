using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Hud.Grid;

namespace HexesOfMortvell.Testing.ActionsTest
{
	public class ActionsTestSelectMoveDestState : FsmState
	{
		public ActionsTestPlayerOrder playerOrder;
		public ActionsTestCellClickListener clickListener;

		private IDisposable moveTargetHighlightRegion;
		private IEnumerable<BoardCell> moveTargets;

		public override void Enter()
		{
			var unit = this.playerOrder.selectedUnit;
			var cellContent = unit.GetComponent<BoardCellContent>();
			var unitAsUnit = unit.GetComponent<ActionsTestUnit>();
			bool canMove = unitAsUnit != null;
			canMove &= unitAsUnit.movementPoints > 0;
			moveTargets = null;
			if (canMove)
			{
				moveTargets = unitAsUnit
					.GetReachableCells(cellContent.Cell)
					.ToList();
				canMove &= moveTargets.Count() > 1;
			}
			if (!canMove)
			{
				this.fsm.Transition<ActionsTestSelectAttackTargetState>();
				return;
			}
			this.moveTargetHighlightRegion = moveTargets.AddHighlightLayer(Color.white);
			this.clickListener.cellClickedEvent += TryMoveToCell;
		}

		public override void Exit()
		{
			this.moveTargetHighlightRegion?.Dispose();
			this.clickListener.cellClickedEvent -= TryMoveToCell;
		}

		void TryMoveToCell(BoardCell cell)
		{
			if (this.moveTargets.Contains(cell))
			{
				this.playerOrder.selectedUnit.Cell.MoveContentTo(cell);
				this.playerOrder.selectedUnit.GetComponent<ActionsTestUnit>().hasMoved = true;
				this.playerOrder.moveDest = cell;
				this.fsm.Transition<ActionsTestSelectAttackTargetState>();
			}
		}
	}
}