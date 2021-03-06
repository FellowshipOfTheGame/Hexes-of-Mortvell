﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Hud;
using HexesOfMortvell.Hud.Grid;
using HexesOfMortvell.Hud.Actions;

namespace HexesOfMortvell.GameModes.Battle
{
	public class BattleSelectUnitActionTargetsState : FsmState
	{
		[Header("References")]
		public BattlePlayerOrders playerOrders;
		public CellClickListener cellClickListener;
		public CellHoverListener cellHoverListener;
		public ColorReference validTargetColor;

		private ISet<BoardCell> validNextTargets;
		private IDisposable validNextTargetsHighlight;

		private ActionTargetFilter[] targetFilters;
		private IDisposable aoeHighlight;
		private BoardCell hoveredCell;

		public override void Enter()
		{
			RetrieveTargetFilters();
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

		void Update()
		{
			if (InputCancel())
				Undo();
		}

		bool InputCancel()
		{
			return Input.GetKeyDown(KeyCode.Backspace)
					|| Input.GetKeyDown(KeyCode.Escape)
					|| Input.GetMouseButtonDown(1);
		}

		void Undo()
		{
			this.fsm.Transition<BattleSelectUnitActionState>();
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

		void RetrieveTargetFilters()
		{
			this.targetFilters = this.playerOrders.action
				.GetComponents<ActionTargetFilter>();
		}

		void PrepareForNextTarget()
		{
			RemoveValidTargetHighlight();

			var currentTargetCount = this.playerOrders.actionTargets.Count;

			if (currentTargetCount == targetFilters.Length)
			{
				this.FinishTargetSelection();
				return;
			}

			var targetFilter = targetFilters[currentTargetCount];

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
			var actor = this.playerOrders.unit.AsCellContent.Cell.Content;
			var currentTargets = this.playerOrders.actionTargets;
			if (currentTargets.Count < targetFilters.Length - 1)
				// More than 1 target left to be selected, can't highlight
				// aoe because it can't be calculated
				return;
			var aoeComponent = action.GetComponent<ActionAoe>();
			var actionHighlighter = action.GetComponent<ActionHighlight>();
			var targetsWithHoveredCell = currentTargets
				.Concat(new[] {this.hoveredCell});
			var aoe = new HashSet<BoardCell>(
				aoeComponent.GetAoe(actor, targetsWithHoveredCell));
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
