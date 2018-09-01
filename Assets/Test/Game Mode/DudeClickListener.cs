using System;
using UnityEngine;
using HexCasters.DesignPatterns.Observer;

namespace HexCasters.Testing.GameModeTest
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