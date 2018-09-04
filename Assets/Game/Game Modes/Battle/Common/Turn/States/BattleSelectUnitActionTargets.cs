using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexCasters.DesignPatterns.Fsm;
using HexCasters.Core.Actions;
using HexCasters.Core.Grid;
using HexCasters.GameModes.Common;
using HexCasters.Hud.Grid;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattleSelectUnitActionTargets : FsmState
	{
		public BattlePlayerOrders playerOrders;
		public CellClickListener cellClickListener;
		private ISet<BoardCell> validNextTargets;
		private IDisposable validNextTargetsHighlight;

		public override void Enter()
		{
			Debug.Log(GetType());
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
			var filterTargets = targetFilter.ValidTargets(
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
