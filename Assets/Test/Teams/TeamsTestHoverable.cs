using System;
using UnityEngine;
using UnityEngine.UI;
using HexesOfMortvell.Core.Units.Teams;
using HexesOfMortvell.Hud.Teams;

namespace HexesOfMortvell.Testing.TeamsTest
{
	[RequireComponent(typeof(TeamMember))]
	public class TeamsTestHoverable : MonoBehaviour
	{
		public Image image;
		private TeamColor teamColor;

		void Start()
		{
			var team = GetComponent<TeamMember>().team;
			this.teamColor = team.GetComponent<TeamColor>();
			ErrorIfNoTeamColor();
		}

		void OnMouseOver()
		{
			this.image.color = this.teamColor.color.Value;
		}

		void ErrorIfNoTeamColor()
		{
			if (teamColor == null)
				throw new ArgumentException(
					"Team has no color");
		}
	}
}
