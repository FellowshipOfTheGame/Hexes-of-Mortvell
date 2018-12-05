using System.Collections;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.VictoryConditions;
using HexesOfMortvell.Core.Units.Teams;
using HexesOfMortvell.GameModes.Battle;

namespace HexesOfMortvell.GameModes.Challenges
{
	public class ChallengeReferee : GameModeReferee
	{
		EndTurnListener endTurnListener;
		Turn turn;
		FiniteStateMachine turnFsm;
		Team playerTeam;
		Team enemyTeam;

		public int playerTeamIndex = 0;

		private int startingNumOfPlayerUnits;

		void Awake()
		{
			this.turn = GameObject.FindObjectOfType<Turn>();
			this.endTurnListener =
				GameObject.FindObjectOfType<EndTurnListener>();

			this.endTurnListener.turnEndedEvent += ProcessEndTurn;

			var teams = this.turn.teamsWithTurns.teams;
			this.playerTeam = teams[this.playerTeamIndex];
			this.enemyTeam = this.turn.TeamFollowing(this.playerTeam);
			this.startingNumOfPlayerUnits = CountPlayerUnits();

			this.turnFsm = this.turn.transform
				.Find("FSM").GetComponent<FiniteStateMachine>();
		}

		int CountPlayerUnits()
		{
			return this.playerTeam.Members.Count;
		}

		int CountEnemyUnits()
		{
			return this.enemyTeam.Members.Count;
		}

		void ProcessEndTurn()
		{
			if (turn.CurrentTeamIndex == this.playerTeamIndex)
				ProcessEnemyTurn();
			else
				ProcessPlayerTurn();
		}

		void ProcessPlayerTurn()
		{
			if (CountPlayerUnits() != this.startingNumOfPlayerUnits)
				AwardVictoryTo(this.enemyTeam);
			else if (CountEnemyUnits() > 0)
				AwardVictoryTo(this.enemyTeam);
			else
				AwardVictoryTo(this.playerTeam);
		}

		void ProcessEnemyTurn()
		{
			StartCoroutine(WaitAndPass());
		}

		IEnumerator WaitAndPass()
		{
			yield return null;
			this.turnFsm.Transition<BattleEndTurnState>();
		}

		public void ProcessVictory(Team team)
		{
			if (team == this.playerTeam)
				Victory();
			else
				Defeat();
		}

		void Defeat()
		{
			Debug.Log("DEFEAT");
		}

		void Victory()
		{
			Debug.Log("VICTORY");
		}
	}
}
