using UnityEngine;
using UnityEngine.EventSystems;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes.Common
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