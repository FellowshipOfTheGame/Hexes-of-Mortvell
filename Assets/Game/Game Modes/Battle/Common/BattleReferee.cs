using System.Collections;
using System.Linq;
using UnityEngine;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.Core.Units.Teams;
using HexesOfMortvell.Core.VictoryConditions;
using HexesOfMortvell.GameModes.Battle;

namespace HexesOfMortvell.GameModes.Battle
{
	public class BattleReferee : GameModeReferee
	{
		DeathListener deathListener;
		BattleTurn turn;

		void Awake()
		{
			this.turn = GameObject.FindObjectOfType<BattleTurn>();
			this.deathListener = GameObject.FindObjectOfType<DeathListener>();
			this.deathListener.deathEvent += CheckVictory;
		}

		public void TestPrint(Team winner)
		{
			Debug.Log($"YOU ARE WINNER!!! {winner.gameObject.name}");
		}

		void CheckVictory(HP deadUnitHP)
		{
			var deadUnitAsTeamMember = deadUnitHP.GetComponent<TeamMember>();
			if (deadUnitAsTeamMember == null)
				return;
			var deadUnitTeam = deadUnitAsTeamMember.team;
			StartCoroutine(WaitForDeathAndCheckVictory(deadUnitTeam));
		}

		IEnumerator WaitForDeathAndCheckVictory(Team deadUnitTeam)
		{
			yield return new WaitForEndOfFrame();
			var deadUnitTeammates = deadUnitTeam.Members;
			bool teamHasAnyOrbs = deadUnitTeammates.Any(IsOrb);
			bool teamHasJustOrbsLeft = deadUnitTeammates.All(IsOrb);
			if (!teamHasAnyOrbs || teamHasJustOrbsLeft)
				AwardVictoryTo(this.turn.TeamFollowing(deadUnitTeam));
		}

		bool IsOrb(TeamMember teamMember)
		{
			return teamMember.GetComponent<Orb>() != null;
		}
	}
}
