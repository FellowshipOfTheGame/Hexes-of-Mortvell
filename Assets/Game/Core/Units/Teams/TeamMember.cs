using UnityEngine;

namespace HexesOfMortvell.Core.Units.Teams
{
	/// <summary>
	/// Indicates this GameObject is part of a team.
	/// </summary>
	public class TeamMember : MonoBehaviour
	{
		/// <summary>
		/// The team the object currently belongs to.
		/// </summary>
		[Tooltip("The team the object belongs to.")]
		public Team team;

		private bool wasAddedToTeam;

		void Awake() => TryRegisterTeamIfNotAlreadyIn();

		void Start() => TryRegisterTeamIfNotAlreadyIn();

		void TryRegisterTeamIfNotAlreadyIn()
		{
			if (this.wasAddedToTeam)
				return;
			if (this.team == null)
				return;
			this.wasAddedToTeam = true;
			this.team.Add(this.gameObject);
		}

		void OnDestroy()
		{
			ExitTeam();
		}

		/// <summary>
		/// Removes the object from the current team.
		/// </summary>
		/// <remarks>
		/// Exiting a team destroys the TeamMember component.
		/// </remarks>
		public void ExitTeam()
		{
			this.team.Remove(this);
		}
	}
}