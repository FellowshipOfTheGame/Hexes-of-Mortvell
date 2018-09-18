using System;
using UnityEngine;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.Testing.ActionsTest
{
	public class ActionsTestCellClick : MonoBehaviour
	{

		public event Action<BoardCell> MouseClickEvent;

		public BoardCell cell;

		void OnMouseDown()
		{
			MouseClickEvent?.Invoke(this.cell);
		}
	}
}
