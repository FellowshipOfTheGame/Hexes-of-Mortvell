using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Units;

namespace HexesOfMortvell.GameModes.Battle
{
	public class BattleFinishOrdersState : FsmState
	{
		public BattlePlayerOrders playerOrders;
		public override void Enter()
		{
			MarkUnitInactionable();
			this.fsm.Transition<BattleOverviewState>();
		}

		public override void Exit() {}

		void MarkUnitInactionable()
		{
			this.playerOrders.unit.hasMoved = true;
		}
	}
}