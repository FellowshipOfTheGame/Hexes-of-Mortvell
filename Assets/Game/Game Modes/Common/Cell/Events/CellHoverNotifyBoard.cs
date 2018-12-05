using UnityEngine;
using UnityEngine.EventSystems;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes
{
	public class CellHoverNotifyBoard : MonoBehaviour
	{
		public BoardCell cell;
		private CellHoverListener listener;

		void Awake()
		{
			var board = this.cell.board;
			this.listener = board.GetComponent<CellHoverListener>();
		}

		public void HoverEnter()
		{
			this.listener.NotifyHoverEnter(this.cell);
		}
	}
}