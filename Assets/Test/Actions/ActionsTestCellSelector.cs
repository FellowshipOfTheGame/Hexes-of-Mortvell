using UnityEngine;
using HexCasters.Core.Grid;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestCellSelector : MonoBehaviour
	{
		public BoardCell selectedCell;

		public void Initialize(Board board)
		{
			for (int x = board.MinX; x <= board.MaxX; x++)
				for (int y = board.MinY; y <= board.MaxY; y++)
				{
					var cell = board[x, y];
				}
		}
	}
}