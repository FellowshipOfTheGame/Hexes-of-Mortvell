using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexCasters.DesignPatterns.Fsm;
using HexCasters.Core.Actions;
using HexCasters.Core.Grid;
using HexCasters.GameModes.Common;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattleSelectUnitActionTargets : FsmState
	{
		public BattlePlayerOrders playerOrders;
		public CellClickListener cellClickListener;
		private ISet<BoardCell> validNextTargets;

		public override void Enter()
		{
			Debug.Log(GetType());
			RegisterClickHandler();
			PrepareForNextTarget();
		}

		public override void Exit()
		{
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
		}

		void FinishTargetSelection()
		{
			foreach (var cell in this.playerOrders.actionTargets)
				Debug.Log(cell);
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
