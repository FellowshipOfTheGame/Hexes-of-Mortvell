using System.Collections.Generic;
using UnityEngine;
using HexCasters.Core.Units.Teams;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattleTurn : MonoBehaviour
	{
		[Tooltip("Teams which have turns to act.")]
		public List<Team> teamsWithTurns;
		private int currentTeamIndex;

		public Team CurrentTeam
		{
			get { return this.teamsWithTurns[this.currentTeamIndex]; }
		}

		public void NextTeam()
		{
			this.currentTeamIndex++;
			this.currentTeamIndex %= this.teamsWithTurns.Count;
		}
	}
}
