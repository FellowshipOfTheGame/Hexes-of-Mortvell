using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Hud.Grid;

namespace HexesOfMortvell.Testing.ActionsTest
{
	public class ActionsTestSelectAttackTargetState : FsmState
	{
		public ActionsTestPlayerOrder playerOrder;
		public ActionsTestCellHoverListener hoverListener;
		public ActionsTestCellClickListener clickListener;
		private List<BoardCell> validNextTargets;
		private IDisposable validNextTargetsHighlight;
		private IDisposable aoeHighlight;

		public override void Enter()
		{
			this.playerOrder.action = this.playerOrder.selectedUnit
				.GetComponent<ActionsTestActor>().action;
			this.playerOrder.selectedTargets = new List<BoardCell>();
			UpdateValidTargets();
			this.validNextTargetsHighlight = null;
			this.aoeHighlight = null;

			hoverListener.hoverEnterEvent += UpdateAoeHighlight;
			clickListener.cellClickedEvent += AddTarget;
		}

		public override void Exit()
		{
			this.validNextTargetsHighlight?.Dispose();
			this.aoeHighlight?.Dispose();
			hoverListener.hoverEnterEvent -= UpdateAoeHighlight;
			clickListener.cellClickedEvent -= AddTarget;
		}

		void UpdateAoeHighlight(BoardCell hoveredCell)
		{
			this.aoeHighlight?.Dispose();
			this.aoeHighlight = null;
			if (validNextTargets.Contains(hoveredCell))
			{
				var aoeComponent = this.playerOrder.action.GetComponent<ActionAoe>();
				this.playerOrder.aoe = aoeComponent
					.GetAoe(this.playerOrder.selectedUnit.GetComponent<BoardCellContent>(), this.playerOrder.selectedTargets)
					.ToList();
				this.aoeHighlight = this.playerOrder.aoe.AddHighlightLayer(Color.cyan);
			}
		}

		void AddTarget(BoardCell clickedCell)
		{
			if (!this.validNextTargets.Contains(clickedCell))
				return;
			var targetFilters = this.playerOrder.action.GetComponents<ActionTargetFilter>();
			if (this.playerOrder.selectedTargets.Count == targetFilters.Length)
			{
				this.fsm.Transition<ActionsTestPerformActionState>();
				return;
			}
			var currentTargetFilter = targetFilters[this.playerOrder.selectedTargets.Count];
			this.playerOrder.selectedTargets.Add(clickedCell);
		}

		void UpdateValidTargets()
		{
			this.validNextTargetsHighlight?.Dispose();
			var targetFilter = this.playerOrder.action.GetComponent<ActionTargetFilter>();
			this.validNextTargets = targetFilter
				.ValidTargets(
					this.playerOrder.selectedUnit,
					this.playerOrder.selectedTargets)
				.ToList();
			this.validNextTargetsHighlight =
				this.validNextTargets.AddHighlightLayer(Color.blue);
		}
	}
}
