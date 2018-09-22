using UnityEngine;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes.Common
{
	public class CellHoverListener : MonoBehaviour
	{
		public delegate void HoverEnterEventHandler(BoardCell cell);
		public delegate void HoverExitEventHandler(BoardCell cell);

		public event HoverEnterEventHandler hoverEnterEvent;
		public event HoverExitEventHandler hoverExitEvent;

		public void NotifyHoverEnter(BoardCell cell)
		{
			this.hoverEnterEvent?.Invoke(cell);
		}

		public void NotifyHoverExit(BoardCell cell)
		{
			this.hoverExitEvent?.Invoke(cell);
		}
	}
}