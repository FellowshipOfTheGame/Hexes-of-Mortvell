using UnityEngine;
using HexesOfMortvell.Core.Units.Teams;
using HexesOfMortvell.Core.VictoryConditions;

namespace HexesOfMortvell.GameModes.Common
{
	public class BattleReferee : GameModeReferee
	{
		public DeathListener deathListener;
		public TeamGroup teams;

		void Awake()
		{
		}
	}
}
