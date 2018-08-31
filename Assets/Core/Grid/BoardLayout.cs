using System;
using System.Collections.Generic;
using UnityEngine;

namespace HexCasters.Core.Grid
{
	[CreateAssetMenu(
		fileName="New Board Layout",
		menuName="HexCasters/Board Layout")]
	public class BoardLayout : ScriptableObject
	{

		[SerializeField]
		private int _numRows;
		[SerializeField]
		private int _numCols;
		public int NumRows
		{
			get { return _numRows; }
			set { _numRows = value; }
		}
		public int NumCols
		{
			get { return _numCols; }
			set { _numCols = value; }
		}

		[Header("Terrains")]
		public BoardCellTerrain defaultTerrain;
		public List<BoardPosition> nonDefaultTerrainPositions;
		public List<BoardCellTerrain> nonDefaultTerrains;

		public BoardCellTerrain FindTerrainType(BoardPosition position)
		{
			var positionIndex = this.nonDefaultTerrainPositions.IndexOf(
				position);
			if (positionIndex < 0)
				return this.defaultTerrain;
			return this.nonDefaultTerrains[positionIndex];
		}
	}
}