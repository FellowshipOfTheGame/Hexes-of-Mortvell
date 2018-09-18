using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Actions;

namespace HexesOfMortvell.GameModes.Battle.Common
{
	public class BattlePerformActionState : FsmState
	{
		public BattlePlayerOrders playerOrders;

		public override void Enter()
		{
			PerformAction();
			this.fsm.Transition<BattleFinishOrdersState>();
		}

		public override void Exit() {}

		void PerformAction()
		{
			var action = this.playerOrders.action;
			var aoeComponent = action.GetComponent<ActionAoe>();
			var aoe = aoeComponent.GetAoe(this.playerOrders.actionTargets);
			var activeComponent= action.GetComponent<ActionActivation>();
			activeComponent.Perform(
				this.playerOrders.unit.AsCellContent,
				this.playerOrders.actionTargets,
				aoe);
			activeComponent.Cleanup(aoe);
		}
	}
}
