using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Units.Teams;

namespace HexesOfMortvell.GameModes.Battle.Common
{
	public class BattleTurn : MonoBehaviour
	{
		[Tooltip("Teams which have turns to act.")]
		public TeamGroup teamsWithTurns;

		[SerializeField]
		private int currentTeamIndex;

		public Team CurrentTeam
		{
			get { return this.teamsWithTurns.teams[this.currentTeamIndex]; }
		}

		public Team TeamFollowing(Team team)
		{
			int teamIndex = this.teamsWithTurns.teams.IndexOf(team);
			if (teamIndex < 0)
				return null;
			int followingTeamIndex = NextTeamIndex(teamIndex);
			return this.teamsWithTurns.teams[followingTeamIndex];
		}

		public void NextTeam()
		{
			this.currentTeamIndex = NextTeamIndex(this.currentTeamIndex);
		}

		int NextTeamIndex(int teamIndex)
		{
			return (teamIndex + 1) % this.teamsWithTurns.teams.Count;
		}
	}
}
