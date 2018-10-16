using System;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Grid.Loading;

namespace HexesOfMortvell.Core.Grid
{
	public class Board : MonoBehaviour
	{
		[Header("Prefabs")]
		public GameObject cellPrefab;

		private BoardCell[,] cells;

		// Overrides the default behaviour to GetComponent<Transform> on
		// every access.
		public Transform Transform
		{
			get;
			private set;
		}

		void Awake()
		{
			this.Transform = GetComponent<Transform>();
		}

		/// <summary>
		/// Number of rows of the loaded layout.
		/// </summary>
		public int NumRows
		{
			get { return cells.GetLength(0); }
		}

		/// <summary>
		/// Number of rows of the loaded layout.
		/// </summary>
		public int NumCols
		{
			get { return cells.GetLength(1); }
		}

		/// <summary>
		/// Minimum X value within bounds of the grid.
		/// </summary>
		public int MinX
		{
			get { return -(this.NumCols / 2); }
		}

		/// <summary>
		/// Maximum X value within bounds of the grid.
		/// </summary>
		public int MaxX
		{
			get { return this.NumCols / 2; }
		}

		/// <summary>
		/// Minimum Y value within bounds of the grid.
		/// </summary>
		public int MinY
		{
			get { return -(this.NumRows / 2); }
		}

		/// <summary>
		/// Maximum Y value within bounds of the grid.
		/// </summary>
		public int MaxY
		{
			get { return this.NumRows / 2; }
		}

		public delegate void BoardLoadedEventHandler(
			Board board, BoardLayout layout);
		/// <summary>
		/// Executed once a layout has been loaded.
		/// </summary>
		public event BoardLoadedEventHandler doneLoadingEvent;

		/// <summary>
		/// Retrieves the cell at the specified position.
		/// </summary>
		public BoardCell this[BoardPosition position]
		{
			get { return GetCell(position); }
			private set { SetCell(position, value); }
		}

		/// <summary>
		/// Retrieves the cell at the specified position.
		/// </summary>
		public BoardCell this[int x, int y]
		{
			get { return GetCell(new BoardPosition(x, y)); }
			private set { SetCell(new BoardPosition(x, y), value); }
		}


		/// <summary>
		/// Loads in a given layout.
		/// </summary>
		/// <param name="layout">The layout to be loaded.</param>
		public void LoadLayout(BoardLayout layout)
		{
			this.cells = new BoardCell[layout.NumRows, layout.NumCols];
			for (int i = 0; i < NumRows; i++)
				for (int j = 0; j < NumCols; j++)
					CreateCell(layout, i, j);
			Debug.Log("Done loading");
			this.doneLoadingEvent?.Invoke(this, layout);
			this.doneLoadingEvent = null;
		}

		void CreateCell(BoardLayout layout, int row, int col)
		{
			var newCellObject = Instantiate(cellPrefab, this.Transform);
			var newCell = newCellObject.GetComponent<BoardCell>();
			var newCellPosition = MatrixIndicesToBoardPosition(row, col);
			newCell.Position = newCellPosition;
			newCell.Terrain = layout.FindTerrainType(newCellPosition);
			this.cells[row, col] = newCell;
		}

		Tuple<int, int> BoardPositionToMatrixIndices(
			BoardPosition position)
		{
			int centerRow = this.NumRows / 2;
			int centerCol = this.NumCols / 2;
			return Tuple.Create(
				centerRow + position.Y,
				centerCol + position.X);
		}

		BoardPosition MatrixIndicesToBoardPosition(int row, int col)
		{
			int centerRow = this.NumRows / 2;
			int centerCol = this.NumCols / 2;
			return new BoardPosition(
				col - centerCol,
				row - centerRow);
		}

		/// <summary>
		/// Retrieves the cell at the specified position.
		/// </summary>
		/// <param name="position">The position to get the cell from</param>
		/// <returns>Returns the specified cell</returns>
		public BoardCell GetCell(BoardPosition position)
		{
			var matrixCoords = BoardPositionToMatrixIndices(position);
			return this.cells[matrixCoords.Item1, matrixCoords.Item2];
		}

		void SetCell(BoardPosition position, BoardCell cell)
		{
			var matrixCoords = BoardPositionToMatrixIndices(position);
			this.cells[matrixCoords.Item1, matrixCoords.Item2] = cell;
		}

		/// <summary>
		/// Instantiates a prefab.
		/// </summary>
		/// <param name="prefab"></param>
		/// <param name="position"></param>
		/// <returns></returns>
		/// <remarks>
		/// <para>
		/// The prefab must have a BoardCellContent component. If it does not,
		/// an ArgumentException will be thrown.
		/// </para>
		/// <para>
		/// If the cell is occupied, an InvalidOperationException will be
		/// thrown.
		/// </para>
		/// <para>
		/// The instantiaded object will be set as the content of
		/// the given cell.
		/// </para>
		/// </remarks>
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

		/// <summary>
		/// Transfers the contents from one cell to the other.
		/// </summary>
		/// <param name="from">The cell which holds the content to be transfered.</param>
		/// <param name="to">The transfer destination.</param>
		/// <remarks>
		/// <para>
		/// If the origin cell is empty, an InvalidOperationException will be thrown.
		/// </para>
		/// <para>
		/// If the destination cell is not empty, an InvalidOperationException
		/// will be thrown.
		/// </para>
		/// </remarks>
		public void MoveContent(BoardPosition from, BoardPosition to)
		{
			this[from].MoveContentTo(this[to]);
		}
	}
}