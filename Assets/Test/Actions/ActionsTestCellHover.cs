using System;
using UnityEngine;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.Testing.ActionsTest
{
	[RequireComponent(typeof(BoardCell))]
	public class ActionsTestCellHover : MonoBehaviour
	{
		public event Action<BoardCell> MouseEnterEvent;
		public event Action<BoardCell> MouseExitEvent;

		public BoardCell cell;

		void OnMouseEnter()
		{
			MouseEnterEvent?.Invoke(this.cell);
		}

		void OnMouseExit()
		{
			MouseExitEvent?.Invoke(this.cell);
		}
	}
}