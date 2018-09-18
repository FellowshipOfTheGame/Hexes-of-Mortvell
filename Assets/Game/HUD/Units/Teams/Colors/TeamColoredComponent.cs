using System;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Observer;
using HexesOfMortvell.Core.Units.Teams;

namespace HexesOfMortvell.Hud.Teams
{
	public abstract class TeamColoredComponent : MonoBehaviour
	{

		public Team team;
		private TeamColor teamColor;
		private IDisposable subscription;

		protected void Start()
		{
			if (this.team == null)
				this.team = GetComponent<TeamMember>().team;
			this.teamColor = team.GetComponent<TeamColor>();
			ErrorIfNoTeamColor();
			var handler = new ValueObserver<Color>(
				nextEventHandler: UpdateColor);
			this.subscription = teamColor.AsObservable.Subscribe(handler);
			UpdateColor(teamColor.Color);
		}

		protected void OnDestroy()
		{
			subscription.Dispose();
		}

		void ErrorIfNoTeamColor()
		{
			if (this.teamColor == null)
				throw new ArgumentException(
					$"Team has no {nameof(TeamColor)} component");
		}

		public abstract void UpdateColor(Color color);
	}
}
