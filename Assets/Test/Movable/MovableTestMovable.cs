using UnityEngine;
using System.Collections.Generic;
using HexCasters.Core.Grid;
using HexCasters.Core.Grid.Regions;

namespace HexCasters.Testing.NeighborhoodBfs
{
	[RequireComponent(typeof(BoardCellContent))]
	public class MovableTestMovable : MonoBehaviour
	{
		public int movementPoints;

		private BoardCellContent asCellContent;

		void Awake()
		{
			this.asCellContent = GetComponent<BoardCellContent>();
		}

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
