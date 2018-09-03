using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexCasters.DesignPatterns.Fsm;
using HexCasters.Core.Grid;
using HexCasters.Core.Actions;
using HexCasters.Hud.Grid;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestSelectAttackTargetState : FsmState
	{
		public ActionsTestPlayerOrder playerOrder;
		public ActionsTestCellHoverListener hoverListener;
		public ActionsTestCellClickListener clickListener;
		private List<BoardCell> selectedTargets;
		private GameObject action;
		private List<BoardCell> validNextTargets;
		private IDisposable validNextTargetsHighlight;
		private IDisposable aoeHighlight;

		public override void Enter()
		{
			this.action = this.playerOrder.selectedUnit
				.GetComponent<ActionsTestActor>().action;
			this.selectedTargets = new List<BoardCell>();
			UpdateValidTargets();
			hoverListener.hoverEnterEvent += UpdateAoeHighlight;
			clickListener.cellClickedEvent += AddTarget;
		}

		public override void Exit()
		{
			hoverListener.hoverEnterEvent -= UpdateAoeHighlight;
			clickListener.cellClickedEvent -= AddTarget;
		}

		void UpdateAoeHighlight(BoardCell hoveredCell)
		{
			this.aoeHighlight?.Dispose();
			if (validNextTargets.Contains(hoveredCell))
			{
				var aoeComponent = this.action.GetComponent<ActionAoe>();
				var aoe = aoeComponent
					.GetAoe(this.selectedTargets)
					.ToList();
				this.aoeHighlight = aoe.Highlight(Color.cyan);
			}
		}

		void AddTarget(BoardCell clickedCell)
		{
			if (!this.validNextTargets.Contains(clickedCell))
				return;
			var targetFilter = this.action.GetComponent<ActionTargetFilter>();
			this.selectedTargets.Add(clickedCell);
			if (this.selectedTargets.Count == targetFilter.targetCount)
				this.fsm.Transition<ActionsTestPerformActionState>();
		}

		void UpdateValidTargets()
		{
			this.validNextTargetsHighlight?.Dispose();
			var targetFilter = this.action.GetComponent<ActionTargetFilter>();
			this.validNextTargets = targetFilter
				.ValidTargets(
					this.playerOrder.selectedUnit,
					this.selectedTargets)
				.ToList();
			this.validNextTargetsHighlight =
				this.validNextTargets.Highlight(Color.blue);
		}
	}
}
