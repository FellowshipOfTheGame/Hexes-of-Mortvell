using UnityEngine;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes.Common
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