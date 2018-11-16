using System;
using UnityEngine;

namespace HexesOfMortvell.GameModes
{
	public class EndTurnListener : MonoBehaviour
	{
		public event Action turnEndedEvent;

		public void NotifyTurnEnd()
		{
			this.turnEndedEvent?.Invoke();
		}
	}
}
