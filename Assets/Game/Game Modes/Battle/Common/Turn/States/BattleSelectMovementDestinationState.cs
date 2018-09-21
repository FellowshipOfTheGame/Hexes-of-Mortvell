﻿using System;
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
		[Header("References")]
		public BattlePlayerOrders playerOrders;
		public CellClickListener clickListener;

		[Header("Values")]
		public Color reachableCellColor;

		private IDisposable reachableCellsHighlight;
		private ISet<BoardCell> reachableCells;

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
			this.reachableCells = new HashSet<BoardCell>(unit.ReachableCells());
		}

		void RegisterClickHandler()
		{
			this.clickListener.clickEvent += TrySelectMovementDest;
		}

		void UnregisterClickHandler()
		{
			this.clickListener.clickEvent -= TrySelectMovementDest;
		}

		void TrySelectMovementDest(BoardCell cell)
		{
			if (this.reachableCells.Contains(cell))
				MoveUnitTo(cell);
		}

		void MoveUnitTo(BoardCell cell)
		{
			this.playerOrders.movementDestination = cell;
			this.fsm.Transition<BattleMoveUnitState>();
		}

		void ApplyReachableHighlight()
		{
			this.reachableCellsHighlight = this.reachableCells
				.AddHighlightLayer(reachableCellColor);
		}

		void RemoveReachableHighlight()
		{
			this.reachableCellsHighlight?.Dispose();
			this.reachableCellsHighlight = null;
		}
	}
}
