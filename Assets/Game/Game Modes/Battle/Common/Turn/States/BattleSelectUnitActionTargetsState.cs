using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.GameModes.Common;
using HexesOfMortvell.Hud.Grid;

namespace HexesOfMortvell.GameModes.Battle.Common
{
	public class BattleSelectUnitActionTargetsState : FsmState
	{
		public BattlePlayerOrders playerOrders;
		public CellClickListener cellClickListener;
		private ISet<BoardCell> validNextTargets;
		private IDisposable validNextTargetsHighlight;
		private ActionTargetFilter[] targetFilters;

		public override void Enter()
		{
			Debug.Log(GetType());
			RetrieveTargetFilters();
			RegisterClickHandler();
			PrepareForNextTarget();
		}

		public override void Exit()
		{
			RemoveValidTargetHighlight();
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
			var currentTargetFilter = targetFilters[currentTargetCount];
			var filterTargets = currentTargetFilter.ValidTargets(
				this.playerOrders.unit.AsCellContent,
				this.playerOrders.actionTargets);
			this.validNextTargets = new HashSet<BoardCell>(filterTargets);
			ApplyValidTargetHighlight();
		}

		void ApplyValidTargetHighlight()
		{
			this.validNextTargetsHighlight = this.validNextTargets
				.AddHighlightLayer(Color.white);
		}

		void RemoveValidTargetHighlight()
		{
			this.validNextTargetsHighlight?.Dispose();
			this.validNextTargetsHighlight = null;
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
	}
}
