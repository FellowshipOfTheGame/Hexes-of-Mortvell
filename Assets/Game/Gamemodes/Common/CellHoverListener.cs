using UnityEngine;
using HexCasters.Core.Grid;

namespace HexCasters.GameModes.Common
{
	public class CellHoverListener : MonoBehaviour
	{
		public delegate void HoverEnterEventHandler(BoardCell cell);
		public event HoverEnterEventHandler hoverEnterEvent;

		public void Notify(BoardCell cell)
		{
			this.hoverEnterEvent?.Invoke(cell);
		}
	}
}