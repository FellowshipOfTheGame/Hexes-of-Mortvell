using UnityEngine;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes.Common
{
	public class EndTurnNotifyCells : MonoBehaviour
	{
		public Board board;

		public void NotifyCells()
		{
			for (var x = this.board.MinX; x <= this.board.MaxX; x++)
				for (var y = this.board.MinY; y <= this.board.MaxY; y++)
					NotifyCell(this.board[x, y]);
		}

		void NotifyCell(BoardCell cell)
		{
			var endTurnListener =
				cell.eventListener.GetComponent<EndTurnListener>();
			endTurnListener.NotifyTurnEnd();
		}
	}
}
