using UnityEngine;
using HexCasters.DesignPatterns.Fsm;
using HexCasters.Core.Grid;
using HexCasters.Core.Units.Teams;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattleBoardSetupState : FsmState
	{
		public LayoutTeamAssigner teamAssigner;

		public override void Enter()
		{
			Debug.Log(GetType().Name);
			this.teamAssigner.doneAssigningTeams += StartFirstTurn;
		}

		public override void Exit()
		{
			this.teamAssigner.doneAssigningTeams -= StartFirstTurn;
		}

		void StartFirstTurn()
		{
			this.fsm.Transition<BattleStartTurnState>();
		}
	}
}