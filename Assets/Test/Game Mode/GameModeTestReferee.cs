using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using HexCasters.Core.Units.Teams;
using HexCasters.DesignPatterns.Observer;
using HexCasters.DesignPatterns.Fsm;

namespace HexCasters.Testing.GameModeTest
{
	public class GameModeTestReferee : MonoBehaviour
	{
		public List<Team> teams;
		public GameModeTestTurn turn;
		public FiniteStateMachine fsm;

		List<IDisposable> observableTeamSubscriptions;

		void Start()
		{
			var observableTeamMemberLists = teams
				.Select(team => team.AsObservable)
				.ToList();
			var teamChangedHandler = new ValueObserver<IList<TeamMember>>(
				nextEventHandler: TeamChangedEvent);
			this.observableTeamSubscriptions = new List<IDisposable>();
			foreach (var memberList in observableTeamMemberLists)
			{
				var subscription = memberList.Subscribe(teamChangedHandler);
				this.observableTeamSubscriptions.Add(subscription);
			}
		}

		void OnDestroy()
		{
			foreach (var subscription in this.observableTeamSubscriptions)
				subscription.Dispose();
		}

		void TeamChangedEvent(IList<TeamMember> members)
		{
			if (members.Count == 0)
				EndGame();
		}

		void EndGame()
		{
			Debug.Log(turn.CurrentTeam);
			this.fsm.Transition<GameModeTestEndGameState>();
		}
	}
}
