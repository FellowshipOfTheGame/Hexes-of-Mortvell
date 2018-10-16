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
			Debug.Log("Enter setup");
			if (this.teamAssigner.doneAssigningTeams)
				StartFirstTurn();
			else
				this.teamAssigner.doneAssigningTeamsEvent += StartFirstTurn;
		}

		public override void Exit()
		{
			Debug.Log("Exit setup");
			this.teamAssigner.doneAssigningTeamsEvent -= StartFirstTurn;
		}

		void StartFirstTurn()
		{
			Debug.Log("Done assigning notification");
			this.fsm.Transition<BattleStartTurnState>();
		}
	}
}