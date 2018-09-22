using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.GameModes.Common;
using HexesOfMortvell.Hud.Grid;
using HexesOfMortvell.Hud.Actions;

namespace HexesOfMortvell.GameModes.Battle.Common
{
	public class BattleSelectUnitActionTargetsState : FsmState
	{
		[Header("References")]
		public BattlePlayerOrders playerOrders;
		public CellClickListener cellClickListener;
		public CellHoverListener cellHoverListener;

		[Header("Values")]
		public Color validTargetColor;

		private ISet<BoardCell> validNextTargets;
		private IDisposable validNextTargetsHighlight;
		private IDisposable aoeHighlight;
		private BoardCell hoveredCell;

		public override void Enter()
		{
			RegisterClickHandler();
			RegisterHoverHandler();
			PrepareForNextTarget();
		}

		public override void Exit()
		{
			ClearAoeHighlight();
			RemoveValidTargetHighlight();
			UnregisterHoverHandler();
			UnregisterClickHandler();
		}

		void TrySelectTarget(BoardCell cell)
		{
			if (this.validNextTargets.Contains(cell))
				SelectTarget(cell);
		}

		void SelectTarget(BoardCell cell)
		{
			this.playerOrders.actionTargets.Add(cell);
			PrepareForNextTarget();
		}

		void PrepareForNextTarget()
		{
			RemoveValidTargetHighlight();

			var targetFilter = this.playerOrders.action
				.GetComponent<ActionTargetFilter>();
			var currentTargetCount = this.playerOrders.actionTargets.Count;

			if (currentTargetCount == targetFilter.TargetCount)
			{
				this.FinishTargetSelection();
				return;
			}

			FindValidTargets(targetFilter);
			ApplyValidTargetHighlight();
		}

		void FindValidTargets(ActionTargetFilter targetFilter)
		{
			var filterTargets = targetFilter.ValidTargets(
				this.playerOrders.unit.AsCellContent,
				this.playerOrders.actionTargets);
			this.validNextTargets = new HashSet<BoardCell>(filterTargets);
		}

		void ApplyValidTargetHighlight()
		{
			this.validNextTargetsHighlight = this.validNextTargets
				.AddHighlightLayer(validTargetColor);
		}

		void RemoveValidTargetHighlight()
		{
			this.validNextTargetsHighlight?.Dispose();
			this.validNextTargetsHighlight = null;
		}

		void UpdateHoveredCell(BoardCell cell)
		{
			this.hoveredCell = cell;
		}

		void UpdateAoeHighlight(BoardCell hoveredCell)
		{
			ClearAoeHighlight();
			if (!this.validNextTargets.Contains(hoveredCell))
				return;
			var action = this.playerOrders.action;
			var targetFilter = action.GetComponent<ActionTargetFilter>();
			var currentTargets = this.playerOrders.actionTargets;
			if (currentTargets.Count < targetFilter.TargetCount - 1)
				// More than 1 target left to be selected, can't highlight
				// aoe because it can't be calculated
				return;
			var aoeComponent = action.GetComponent<ActionAoe>();
			var actionHighlighter = action.GetComponent<ActionHighlight>();
			var targetsWithHoveredCell = currentTargets
				.Concat(new[] {this.hoveredCell});
			var aoe = new HashSet<BoardCell>(
				aoeComponent.GetAoe(targetsWithHoveredCell));
			var aoeColors = actionHighlighter.GetColors(aoe);
			this.aoeHighlight = aoe.AddHighlightLayer(aoeColors);
		}

		void ClearAoeHighlight()
		{
			this.aoeHighlight?.Dispose();
			this.aoeHighlight = null;
		}

		void FinishTargetSelection()
		{
			this.fsm.Transition<BattlePerformActionState>();
		}

		void RegisterClickHandler()
		{
			this.cellClickListener.clickEvent += TrySelectTarget;
		}

		void UnregisterClickHandler()
		{
			this.cellClickListener.clickEvent -= TrySelectTarget;
		}

		void ClearAoeHighlightOnCellHoverExit(BoardCell cell)
		{
			ClearAoeHighlight();
		}

		void RegisterHoverHandler()
		{
			this.cellHoverListener.hoverEnterEvent += UpdateHoveredCell;
			this.cellHoverListener.hoverEnterEvent += UpdateAoeHighlight;
			this.cellHoverListener.hoverExitEvent +=
				ClearAoeHighlightOnCellHoverExit;
		}

		void UnregisterHoverHandler()
		{
			this.cellHoverListener.hoverExitEvent -=
				ClearAoeHighlightOnCellHoverExit;
			this.cellHoverListener.hoverEnterEvent -= UpdateAoeHighlight;
			this.cellHoverListener.hoverEnterEvent -= UpdateHoveredCell;
		}
	}
}
