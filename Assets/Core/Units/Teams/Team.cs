using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HexCasters.Core.Units.Teams
{
	public class Team : MonoBehaviour
	{
		[SerializeField]
		private List<TeamMember> members;
		public IReadOnlyList<TeamMember> Members
		{
			get { return new ReadOnlyCollection<TeamMember>(members); }
		}

		void Awake()
		{
			this.members = new List<TeamMember>();
		}

		public void Add(GameObject gameObject)
		{
			var teamMembership = GetOrCreateTeamMembership(gameObject);
			ErrorIfMemberOfOtherTeam(teamMembership);
			teamMembership.team = this;
			this.members.Add(teamMembership);
		}

		public void Remove(TeamMember teamMembership)
		{
			ErrorIfMemberOfOtherTeam(teamMembership);
			this.members.Remove(teamMembership);
			teamMembership.team = null;
		}

		TeamMember GetOrCreateTeamMembership(GameObject gameObject)
		{
			var component = gameObject.GetComponent<TeamMember>();
			if (component == null)
				component = gameObject.AddComponent<TeamMember>();
			return component;
		}

		void ErrorIfMemberOfOtherTeam(TeamMember teamMembership)
		{
			if (teamMembership.team == null || teamMembership.team == this)
				return;
			var objName = teamMembership.gameObject.name;
			throw new ArgumentException(
				$"Object \"{objName}\"is already a member of another team");
		}
	}
}