using System;
using UnityEngine;

namespace HexCasters.Core.Board
{
	public class Board : MonoBehaviour
	{
		[Header("Prefabs")]
		public GameObject cellPrefab;

		public BoardCell[,] cells;
		[HideInInspector]
		public new Transform transform;

		void Awake()
		{
			this.transform = GetComponent<Transform>();
		}
		public int NumRows
		{
			get { return cells.GetLength(0); }
		}
		public int NumCols
		{
			get { return cells.GetLength(1); }
		}

		public BoardCell this[BoardPosition position]
		{
			get { return GetCell(position); }
			private set { SetCell(position, value); }
		}


		public void LoadLayout(BoardLayout layout)
		{
			this.cells = new BoardCell[layout.NumRows, layout.NumCols];
			for (int i = 0; i < NumRows; i++)
				for (int j = 0; j < NumCols; j++)
					CreateCell(layout, i, j);
		}

		private void CreateCell(BoardLayout layout, int row, int col)
		{
			var newCellObject = Instantiate(cellPrefab, this.transform);
			var newCell = newCellObject.GetComponent<BoardCell>();
			newCell.Position = MatrixIndicesToBoardPosition(row, col);
		}

		private Tuple<int, int> BoardPositionToMatrixIndices(
			BoardPosition position)
		{
			int centerRow = this.NumRows / 2;
			int centerCol = this.NumCols / 2;
			return Tuple.Create(
				centerRow + position.y,
				centerCol + position.x);
		}

		private BoardPosition MatrixIndicesToBoardPosition(int row, int col)
		{
			int centerRow = this.NumRows / 2;
			int centerCol = this.NumCols / 2;
			return new BoardPosition(
				col - centerCol,
				row - centerRow);
		}

		public BoardCell GetCell(BoardPosition position)
		{
			var matrixCoords = BoardPositionToMatrixIndices(position);
			return this.cells[matrixCoords.Item1, matrixCoords.Item2];
		}

		private void SetCell(BoardPosition position, BoardCell cell)
		{
			var matrixCoords = BoardPositionToMatrixIndices(position);
			this.cells[matrixCoords.Item1, matrixCoords.Item2] = cell;
		}

		public void MoveContent(BoardPosition from, BoardPosition to)
		{
			var content = this[from].GetContent();
			this[from].SetContent(null);
			this[to].SetContent(content);
		}
	}
}