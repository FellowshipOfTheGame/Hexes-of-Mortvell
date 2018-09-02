using System;
using UnityEngine;
using HexCasters.Core.Grid;

namespace HexCasters.Testing.ActionsTest
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
