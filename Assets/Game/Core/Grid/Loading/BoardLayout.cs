using System;
using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Loading
{
	/// <summary>
	/// Represents a board's initial layout, which can be loaded.
	/// </summary>
	[CreateAssetMenu(
		fileName="New Board Layout",
		menuName="HexesOfMortvell/Board Layout")]
	public class BoardLayout : ScriptableObject
	{
		[SerializeField]
		private int _numRows;
		[SerializeField]
		private int _numCols;

		/// <summary>
		/// The number of rows in the board.
		/// </summary>
		public int NumRows
		{
			get { return _numRows; }
			set { _numRows = value; }
		}

		/// <summary>
		/// The number of columns in the layout.
		/// </summary>
		public int NumCols
		{
			get { return _numCols; }
			set { _numCols = value; }
		}

		/// <summary>
		/// Terrain for positions whose terrain types were not speficied.
		/// </summary>
		[Header("Terrains")]
		[Tooltip("If no specified terrain is given for a position, this terrain type will be used.")]
		public BoardCellTerrain defaultTerrain;

		/// <summary>
		/// Positions whose terrain types are not the default.
		/// </summary>
		[Tooltip("List of positions whose terrain types are not the default.")]
		public List<BoardPosition> nonDefaultTerrainPositions;

		/// <summary>
		/// Respective terrain types for the positions in nonDefaultTerrainPositions.
		/// </summary>
		[Tooltip("Respective terrains of the positions above.")]
		public List<BoardCellTerrain> nonDefaultTerrains;

		[Tooltip("Positions of spawn points.")]
		public List<BoardPosition> spawnPositions;

		[Serializable]
		public class SpawnInformation
		{
			public static int NoTeam = -1;
			public bool HasTeam => teamIndex != NoTeam;

			public GameObject prefab;
			public int teamIndex = NoTeam;
		}
		public List<SpawnInformation> spawnInfo;

		/// <summary>
		/// Retrieves the terrain type for a given position.
		/// </summary>
		/// <param name="position">The position whose terrain type is desired.</param>
		/// <returns>The terrain type for the given position.</returns>
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