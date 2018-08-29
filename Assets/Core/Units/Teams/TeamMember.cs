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
	}
}