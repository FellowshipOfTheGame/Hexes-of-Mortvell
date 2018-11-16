using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.GameModes.Battle
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
