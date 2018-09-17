using System;
using System.Collections.Generic;
using UnityEngine;
using HexCasters.DesignPatterns.Fsm;
using HexCasters.Core.Grid;
using HexCasters.Hud.Grid;
using HexCasters.GameModes.Common;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattleSelectMovementDestinationState : FsmState
	{
		public BattlePlayerOrders playerOrders;
		public CellClickListener clickListener;

		private IDisposable reachableCellsHighlight;
		private IEnumerable<BoardCell> reachableCells;

		public override void Enter()
		{
			Debug.Log(GetType());
			SkipStateIfSelectedUnitImmobile();
			// FIXME
			FindReachableRegion();
			ApplyReachableHighlight();
			RegisterClickHandler();
		}

		public override void Exit()
		{
			RemoveReachableHighlight();
			UnregisterClickHandler();
		}

		void SkipStateIfSelectedUnitImmobile()
		{
			if (this.playerOrders.unit.movementPoints == 0)
				this.fsm.Transition<BattleSelectUnitActionState>();
		}

		void FindReachableRegion()
		{
			var unit = this.playerOrders.unit;
			this.reachableCells = unit.ReachableCells();
		}

		void RegisterClickHandler()
		{
			this.clickListener.clickEvent += MoveUnitTo;
		}

		void UnregisterClickHandler()
		{
			this.clickListener.clickEvent -= MoveUnitTo;
		}

		void MoveUnitTo(BoardCell cell)
		{
			this.playerOrders.movementDestination = cell;
			this.fsm.Transition<BattleMoveUnitState>();
		}

		void ApplyReachableHighlight()
		{
			this.reachableCellsHighlight = this.reachableCells
				.AddHighlightLayer(Color.blue);
		}

		void RemoveReachableHighlight()
		{
			this.reachableCellsHighlight?.Dispose();
			this.reachableCellsHighlight = null;
		}
	}
}
