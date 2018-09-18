using System;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Hud.Grid;
using HexesOfMortvell.GameModes.Common;

namespace HexesOfMortvell.GameModes.Battle.Common
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
			if (SelectedUnitIsImmobile())
			{
				SkipState();
				return;
			}
			FindReachableRegion();
			ApplyReachableHighlight();
			RegisterClickHandler();
		}

		public override void Exit()
		{
			RemoveReachableHighlight();
			UnregisterClickHandler();
		}

		void SkipState()
		{
			this.fsm.Transition<BattleSelectUnitActionState>();
		}

		bool SelectedUnitIsImmobile()
		{
			return this.playerOrders.unit.movementPoints == 0;
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
