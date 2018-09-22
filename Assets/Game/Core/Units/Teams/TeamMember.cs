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

		void Awake()
		{
			if (this.team != null)
				this.team.Add(this.gameObject);
		}

		void OnDestroy()
		{
			Debug.Log("Destroying");
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
			Debug.Log(string.Join(", ", this.team.Members));
			this.team.Remove(this);
			Debug.Log(string.Join(", ", this.team.Members));
		}
	}
}