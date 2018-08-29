using System;
using UnityEngine;
using HexCasters.DesignPatterns.Observer;

namespace HexCasters.Core.Units.Teams
{
	public class TeamColored : MonoBehaviour
	{
		public Team team;
		private TeamColor teamColor;
		private ObservableValue<Color> observableTeamColor;
		public IObservable<Color> Color
		{
			get { return observableTeamColor; }
		}

		void Awake()
		{
			this.teamColor = this.team.GetComponent<TeamColor>();
			this.observableTeamColor = new ObservableValue<Color>();
			ErrorIfNoColorComponent();
		}

		void Start()
		{
			NotifyColorUpdate();
		}

		public void NotifyColorUpdate()
		{
			this.observableTeamColor.Value = teamColor.color;
		}

		void ErrorIfNoColorComponent()
		{
			if (this.teamColor == null)
				throw new ArgumentException(
					$"Team has no {nameof(TeamColor)} component");
		}
	}
}