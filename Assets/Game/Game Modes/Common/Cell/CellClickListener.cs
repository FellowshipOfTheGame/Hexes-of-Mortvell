using UnityEngine;
using HexCasters.Core.Grid;

namespace HexCasters.GameModes.Common
{
	public class CellClickListener : MonoBehaviour
	{
		public delegate void ClickEventHandler(BoardCell cell);
		public event ClickEventHandler clickEvent;

		public void Notify(BoardCell cell)
		{
			this.clickEvent?.Invoke(cell);
		}
	}
}