using UnityEngine;
using UnityEngine.EventSystems;
using HexCasters.Core.Grid;

namespace HexCasters.GameModes.Common
{
	[RequireComponent(typeof(BoardCell))]
	[RequireComponent(typeof(EventTrigger))]
	// [RequireComponent(typeof(CellNotifyOnHover))]
	public class CellHoverNotifyBoard : MonoBehaviour
	{
		public BoardCell cell;
		private CellHoverListener listener;

		void Awake()
		{
			var board = cell.board;
			this.listener = board.GetComponent<CellHoverListener>();
		}

		public void HoverEnter()
		{
			this.listener.Notify(this.cell);
		}
	}
}