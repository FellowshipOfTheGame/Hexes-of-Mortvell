using UnityEngine;
using UnityEngine.EventSystems;
using HexCasters.Core.Grid;

namespace HexCasters.GameModes.Common
{
	public class CellClickNotifyBoard : MonoBehaviour
	{
		public BoardCell cell;
		private CellClickListener listener;

		void Awake()
		{
			var board = this.cell.board;
			this.listener = board.GetComponent<CellClickListener>();
		}

		public void Click()
		{
			this.listener.Notify(this.cell);
		}
	}
}