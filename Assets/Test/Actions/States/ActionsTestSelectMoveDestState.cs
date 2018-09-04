using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexCasters.Core.Grid;
using HexCasters.DesignPatterns.Fsm;
using HexCasters.Hud.Grid;

namespace HexCasters.Testing.ActionsTest
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
			var unitAsMovable = unit.GetComponent<ActionsTestMovable>();
			bool canMove = unitAsMovable != null;
			canMove &= unitAsMovable.movementPoints > 0;
			moveTargets = null;
			if (canMove)
			{
				moveTargets = unitAsMovable
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
				this.playerOrder.selectedUnit.GetComponent<ActionsTestMovable>().hasMoved = true;
				this.playerOrder.moveDest = cell;
				this.fsm.Transition<ActionsTestSelectAttackTargetState>();
			}
		}
	}
}