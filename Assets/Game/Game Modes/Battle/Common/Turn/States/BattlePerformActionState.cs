using HexCasters.DesignPatterns.Fsm;
using HexCasters.Core.Actions;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattlePerformActionState : FsmState
	{
		public BattlePlayerOrders playerOrders;

		public override void Enter()
		{
			PerformAction();
			this.fsm.Transition<BattleOverviewState>();
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
		}
	}
}
