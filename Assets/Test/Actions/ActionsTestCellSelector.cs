using UnityEngine;
using System.Collections.Generic;
using HexCasters.Core.Grid;
using HexCasters.Hud.Grid;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestCellSelector : MonoBehaviour
	{
		private Board board;
		public BoardCell selectedCell;

		public void Initialize(Board board)
		{
			this.board = board;
			for (int x = board.MinX; x <= board.MaxX; x++)
				for (int y = board.MinY; y <= board.MaxY; y++)
				{
					var cell = board[x, y];
					var hover = cell.GetComponent<ActionsTestCellHover>();
					hover.MouseEnterEvent += HoverEnter;
					hover.MouseExitEvent += HoverExit;
				}
		}

		void OnDestroy()
		{
			for (int x = this.board.MinX; x <= this.board.MaxX; x++)
				for (int y = this.board.MinY; y <= this.board.MaxY; y++)
				{
					var cell = this.board[x, y];
					if (cell != null)
					{
						var hover = cell?.GetComponent<ActionsTestCellHover>();
						hover.MouseEnterEvent -= HoverEnter;
						hover.MouseExitEvent -= HoverExit;
					}
				}
		}

		void HoverEnter(BoardCell cell)
		{
			var highlight = cell.GetComponent<Highlight>();
			highlight.Color = Color.red;
		}

		void HoverExit(BoardCell cell)
		{
			var highlight = cell.GetComponent<Highlight>();
			highlight.Color = Color.clear;
		}
	}
}