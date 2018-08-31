using UnityEngine;
using System.Collections.Generic;
using HexCasters.Core.Grid;
using HexCasters.Core.Grid.Regions;

namespace HexCasters.Core.Units
{
	/// <summary>
	/// Component of GameObjects that can move on the board.
	/// </summary>
	[RequireComponent(typeof(BoardCellContent))]
	public class Movable : MonoBehaviour
	{
		/// <summary>
		/// Amount of points the GameObject can spend per movement.
		/// </summary>
		[Tooltip("Amount of points the GameObject can spend per movement")]
		public int movementPoints;

		private BoardCellContent asCellContent;

		void Awake()
		{
			this.asCellContent = GetComponent<BoardCellContent>();
		}

		/// <summary>
		/// Enumerates over all cells the GameObject can reach.
		/// </summary>
		/// <param name="currentCell">The current cell of the GameObject.</param>
		/// <remarks>
		/// The returned cells are the Neighborhood of the current cell of the
		/// object. The distance function used is the movement cost of the
		/// destination cell, and the maximum distance from the origin is the
		/// number of movement points of this object.
		/// </remarks>
		public IEnumerable<BoardCell> FindReachableRegion()
		{
			var currentCell = this.asCellContent.Cell;
			return currentCell.Neighborhood(
				this.movementPoints,
				DestinationMovementCostAsDistance);
		}

		private static int DestinationMovementCostAsDistance(
			BoardCell from,
			BoardCell to)
		{
			return to.Terrain.movementCost;
		}
	}
}