using UnityEngine;
using System;
using System.Collections.Generic;
using HexCasters.Core.Units.Teams;
using HexCasters.DesignPatterns.Observer;

namespace HexCasters.Testing.GameModeTest
{
	public class GameModeTestTeamSizeChangeListener : MonoBehaviour
	{
		public Team team;
		public GameModeTestReferee referee;
		IDisposable observableTeamSubscription;

		void Start()
		{
			var teamChangedHandler = new ValueObserver<IList<TeamMember>>(
				nextEventHandler: TeamMembersChangedEvent);
			var observableTeam = this.team.AsObservable;
			this.observableTeamSubscription = observableTeam
				.Subscribe(teamChangedHandler);
		}

		void OnDestroy()
		{
			this.observableTeamSubscription.Dispose();
		}

		void TeamMembersChangedEvent(IList<TeamMember> teamMembers)
		{
			if (teamMembers.Count == 0)
				referee.AwardVictoryToCurrentTeam();
		}
	}
}
