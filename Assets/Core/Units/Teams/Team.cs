using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using HexCasters.DesignPatterns.Observer;

namespace HexCasters.Core.Units.Teams
{
	/// <summary>
	/// A group of objects with the TeamMember component.
	/// </summary>
	public class Team : MonoBehaviour
	{
		[SerializeField]
		private List<TeamMember> members;
		private ObservableValue<List<TeamMember>> observableMembers;

		/// <summary>
		/// An IReadOnlyList of the team's current members.
		/// </summary>
		/// <remarks>
		/// This is a costly operation and should not be called in a loop
		/// unless the team is actively being modified.
		/// </remarks>
		public IReadOnlyList<TeamMember> Members
		{
			get { return new ReadOnlyCollection<TeamMember>(observableMembers.Value); }
		}

		public IObservable<IList<TeamMember>> AsObservable
		{
			get { return this.observableMembers; }
		}

		void Awake()
		{
			this.observableMembers = new ObservableValue<List<TeamMember>>();
			this.observableMembers.Value = this.members;
		}

		/// <summary>
		/// Add a GameObject to the team.
		/// </summary>
		/// <param name="gameObject">The GameObject to add to the team.</param>
		/// <remarks>
		/// <para>
		/// If the GameObject does not have a TeamMember component, a new one
		/// will be attached to it.
		/// </para>
		/// <para>
		/// If the GameObject has a TeamMember component and its team is null,
		/// its team property will be set to the object's newly assigned team.
		/// </para>
		/// <para>
		/// If the GameObject has a TeamMember component and its team is not null,
		/// an ArgumentException will be thrown.
		/// </para>
		/// </remarks>
		public void Add(GameObject gameObject)
		{
			var teamMembership = GetOrCreateTeamMembership(gameObject);
			ErrorIfMemberOfOtherTeam(teamMembership);
			teamMembership.team = this;
			this.members.Add(teamMembership);
			this.observableMembers.NotifyValueChange();
		}

		/// <summary>
		/// Removes a GameObject from the team.
		/// </summary>
		/// <param name="teamMembership">The TeamMember component of the object to be removed.</param>
		/// <remarks>
		/// <para>
		/// If teamMembership is null or its team is null, an ArgumentException
		/// will be thrown.
		/// </para>
		/// <para>
		/// If the object belongs to another team, an ArgumentException will be
		/// thrown.
		/// </para>
		/// <para>
		/// After removal, the TeamMember component will be removed.
		/// </para>
		/// </remarks>
		public void Remove(TeamMember teamMembership)
		{
			ErrorIfNullTeam(teamMembership);
			ErrorIfMemberOfOtherTeam(teamMembership);
			this.members.Remove(teamMembership);
			this.observableMembers.NotifyValueChange();
			Destroy(teamMembership);
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

		void ErrorIfNullTeam(TeamMember teamMembership)
		{
			if (teamMembership?.team == null)
				throw new ArgumentException("Object has no team");
		}
	}
}