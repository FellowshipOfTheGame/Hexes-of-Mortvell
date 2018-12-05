using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Units.Teams;

namespace HexesOfMortvell.GameModes.Battle
{
	public class BattleBoardSetupState : FsmState
	{
		public LayoutTeamAssigner teamAssigner;

		public override void Enter()
		{
			if (this.teamAssigner.doneAssigningTeams)
				StartFirstTurn();
			else
				this.teamAssigner.doneAssigningTeamsEvent += StartFirstTurn;
		}

		public override void Exit()
		{
			this.teamAssigner.doneAssigningTeamsEvent -= StartFirstTurn;
		}

		void StartFirstTurn()
		{
			this.fsm.Transition<BattleStartTurnState>();
		}
	}
}