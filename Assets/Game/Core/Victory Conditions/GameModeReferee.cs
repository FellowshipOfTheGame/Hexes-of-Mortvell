using System;
using UnityEngine;
using UnityEngine.Events;
using HexesOfMortvell.Core.Units.Teams;

namespace HexesOfMortvell.Core.VictoryConditions
{
	public class GameModeReferee : MonoBehaviour
	{
		[Serializable]
		public class VictoryEventHandler : UnityEvent<Team> {}
		public VictoryEventHandler victoryEvent;

		public void AwardVictoryTo(Team winner)
		{
			this.victoryEvent.Invoke(winner);
		}
	}
}