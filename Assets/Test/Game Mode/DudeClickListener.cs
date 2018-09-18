using System;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Observer;

namespace HexesOfMortvell.Testing.GameModeTest
{
	public class DudeClickListener : MonoBehaviour
	{
		public delegate void DudeClickedEventHandler(GameObject dude);
		public event DudeClickedEventHandler dudeClickedEvent;

		public void NotifyClick(GameObject dude)
		{
			this.dudeClickedEvent?.Invoke(dude);
		}
	}
}