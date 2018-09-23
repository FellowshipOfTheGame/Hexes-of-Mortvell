using UnityEngine;
using HexesOfMortvell.Hud.Teams;

namespace HexesOfMortvell.Testing.TeamsTest
{
	public class TeamsTestTeamColorRandomizer : MonoBehaviour
	{
		public TeamColor teamColor;

		private int? curIdx;

		private static Color[] Colors = new Color[]
		{
			Color.red,
			Color.blue,
			Color.green
		};

		void Start()
		{
			Randomize();
		}

		public void Randomize()
		{
			int randomIdx;
			do
			{
				randomIdx = (int) (Random.value * Colors.Length);
			}
			while (randomIdx == curIdx);
			// teamColor.color.Value = Colors[randomIdx];
			curIdx = randomIdx;
		}
	}
}
