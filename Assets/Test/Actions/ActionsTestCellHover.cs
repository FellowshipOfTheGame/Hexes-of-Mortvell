using System;
using UnityEngine;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestCellHover : MonoBehaviour
	{
		public event Action MouseEnterEvent;
		public event Action MouseExitEvent;

		void OnMouseEnter()
		{
			MouseEnterEvent?.Invoke();
		}

		void OnMouseExit()
		{
			MouseExitEvent?.Invoke();
		}
	}
}