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

		public void NextTeam()
		{
			this.currentTeamIndex++;
			this.currentTeamIndex %= this.teamsWithTurns.teams.Count;
		}
	}
}
