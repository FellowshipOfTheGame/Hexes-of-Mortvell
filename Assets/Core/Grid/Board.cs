using System;
using UnityEngine;

namespace HexCasters.Core.Grid
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

		public BoardCell this[int x, int y]
		{
			get { return GetCell(new BoardPosition(x, y)); }
			private set { SetCell(new BoardPosition(x, y), value); }
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
			var newCellPosition = MatrixIndicesToBoardPosition(row, col);
			newCell.Position = newCellPosition;
			newCell.Terrain = layout.FindTerrainType(newCellPosition);
			this.cells[row, col] = newCell;
		}

		private Tuple<int, int> BoardPositionToMatrixIndices(
			BoardPosition position)
		{
			int centerRow = this.NumRows / 2;
			int centerCol = this.NumCols / 2;
			return Tuple.Create(
				centerRow + position.Y,
				centerCol + position.X);
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

		public BoardCellContent Spawn(GameObject prefab, BoardPosition position)
		{
			var cell = this[position];
			var instance = Instantiate(prefab);
			var cellContent = instance.GetComponent<BoardCellContent>();
			if (cellContent == null)
				throw new ArgumentException(
					$"Prefab has no {nameof(BoardCellContent)} component");
			cell.SetContent(cellContent);
			return cellContent;
		}

		public void MoveContent(BoardPosition from, BoardPosition to)
		{
			MoveContent(this[from], this[to]);
		}

		public void MoveContent(BoardCell from, BoardCell to)
		{
			var content = from.GetContent();
			from.SetContent(null);
			to.SetContent(content);
		}
	}
}