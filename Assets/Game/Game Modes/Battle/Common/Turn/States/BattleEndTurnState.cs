using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.GameModes.Battle.Common
{
	public class BattleEndTurnState : FsmState
	{
		public BattleTurn turn;
		public override void Enter()
		{
			this.turn.NextTeam();
			this.fsm.Transition<BattleStartTurnState>();
		}

		public override void Exit() {}
	}
}
