using System.Linq;
using System.Collections;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.VictoryConditions;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.Core.Units.Teams;
using HexesOfMortvell.GameModes.Battle;

namespace HexesOfMortvell.GameModes.Challenges
{
	public class ChallengeReferee : GameModeReferee
	{
		EndTurnListener endTurnListener;
		DeathListener deathListener;
		Turn turn;
		FiniteStateMachine turnFsm;
		Team playerTeam;
		Team enemyTeam;

		public int playerTeamIndex = 0;

		int numOfRemainingEnemies;
		bool allEnemiesDead;

		void Awake()
		{
			this.turn = GameObject.FindObjectOfType<Turn>();
			this.endTurnListener = GameObject.FindObjectOfType<EndTurnListener>();
			this.deathListener = GameObject.FindObjectOfType<DeathListener>();
			this.turnFsm = this.turn.transform.GetComponentInChildren<FiniteStateMachine>();

			this.playerTeam = this.turn.teamsWithTurns.teams[this.playerTeamIndex];
			this.enemyTeam = this.turn.TeamFollowing(this.playerTeam);

			this.deathListener.deathEvent += CheckDeath;
			this.endTurnListener.turnEndedEvent += CheckEndTurn;

			this.numOfRemainingEnemies = this.enemyTeam.Members.Count;
			this.allEnemiesDead = false;
		}

		void CheckDeath(HP unitHP)
		{
			var unitTeam = unitHP.GetComponent<TeamMember>()?.team;
			if (unitTeam == this.playerTeam)
			{
				AwardVictoryTo(this.enemyTeam);
			}
			else
			{
				this.numOfRemainingEnemies--;
				if (this.numOfRemainingEnemies == 0)
					this.allEnemiesDead = true;
			}
		}

		void CheckEndTurn()
		{
			bool endOfPlayerTurn = (this.turn.CurrentTeam == this.playerTeam);
			if (endOfPlayerTurn)
				StartCoroutine(AwaitTurnSwapAndCheckVictory());
		}

		IEnumerator AwaitTurnSwapAndCheckVictory()
		{
			yield return null; // Wait for end of turn processing
			if (this.allEnemiesDead)
				AwardVictoryTo(this.playerTeam);
			else
				AwardVictoryTo(this.enemyTeam);
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
