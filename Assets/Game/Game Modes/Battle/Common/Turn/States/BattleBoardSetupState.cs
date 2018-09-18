using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Units.Teams;

namespace HexesOfMortvell.GameModes.Battle.Common
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