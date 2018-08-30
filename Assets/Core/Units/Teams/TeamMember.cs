using UnityEngine;

namespace HexCasters.Core.Units.Teams
{
	public class TeamMember : MonoBehaviour
	{
		public Team team;

		void Start()
		{
			this.team.Add(this.gameObject);
		}

		void OnDestroy()
		{
			ExitTeam();
		}

		public void ExitTeam()
		{
			this.team.Remove(this);
		}
	}
}