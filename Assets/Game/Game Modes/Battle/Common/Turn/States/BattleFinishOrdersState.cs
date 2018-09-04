using HexCasters.DesignPatterns.Fsm;
using HexCasters.Core.Units;

namespace HexCasters.GameModes.Battle.Common
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