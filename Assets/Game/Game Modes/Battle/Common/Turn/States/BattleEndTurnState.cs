using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.GameModes.Common;

namespace HexesOfMortvell.GameModes.Battle.Common
{
	public class BattleEndTurnState : FsmState
	{
		public BattleTurn turn;
		public EndTurnNotifyCells cellEndTurnNotifier;

		public override void Enter()
		{
			this.cellEndTurnNotifier.NotifyCells();
			this.turn.NextTeam();
			this.fsm.Transition<BattleStartTurnState>();
		}

		public override void Exit() {}
	}
}
