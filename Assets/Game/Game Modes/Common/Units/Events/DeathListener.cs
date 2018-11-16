using UnityEngine;
using HexesOfMortvell.Core.Units;

namespace HexesOfMortvell.GameModes
{
	public class DeathListener : MonoBehaviour
	{
		public delegate void DeathEventHandler(HP hp);
		public event DeathEventHandler deathEvent;

		public void Notify(HP hp)
		{
			this.deathEvent?.Invoke(hp);
		}
	}
}