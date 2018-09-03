using System;
using UnityEngine;
using HexCasters.Core.Grid;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestCellClickListener : MonoBehaviour
	{
		public Board board;

		public event Action<BoardCell> cellClickedEvent;

		void Start()
		{
			board.boardLoadedEvent += Initialize;
		}

		void Initialize(Board board)
		{
			this.board = board;
			for (int x = board.MinX; x <= board.MaxX; x++)
				for (int y = board.MinY; y <= board.MaxY; y++)
				{
					var cell = board[x, y];
					var hover = cell.GetComponent<ActionsTestCellClick>();
					hover.MouseClickEvent += MouseClick;
				}
		}

		void MouseClick(BoardCell cell)
		{
			this.cellClickedEvent?.Invoke(cell);
		}
	}
}
