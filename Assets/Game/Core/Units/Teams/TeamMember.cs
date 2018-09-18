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

		void Start()
		{
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