using System;
using UnityEngine;
using System.Collections.Generic;
using HexCasters.Core.Grid;
using HexCasters.Hud.Grid;

namespace HexCasters.Testing.ActionsTest
{
	// split this up into a hover listener and a hover highlight
	public class ActionsTestCellHoverListener : MonoBehaviour
	{
		public Board board;

		private IDictionary<BoardPosition, IDisposable> cellHoverHighlights;

		public event Action<BoardCell> hoverEnterEvent;
		public event Action<BoardCell> hoverExitEvent;

		void Start()
		{
			board.boardLoadedEvent += Initialize;
		}

		public void Initialize(Board board)
		{
			this.board = board;
			this.cellHoverHighlights =
				new Dictionary<BoardPosition, IDisposable>();
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
						if (this.cellHoverHighlights.ContainsKey(cell.Position))
							this.cellHoverHighlights[cell.Position].Dispose();
					}
				}
		}

		void HoverEnter(BoardCell cell)
		{
			hoverEnterEvent?.Invoke(cell);
			var highlight = cell.GetComponent<LayeredHighlight>();
			var layer = highlight.AddLayer(Color.red);
			this.cellHoverHighlights[cell.Position] = layer;
		}

		void HoverExit(BoardCell cell)
		{
			hoverExitEvent?.Invoke(cell);
			this.cellHoverHighlights[cell.Position].Dispose();
		}
	}
}