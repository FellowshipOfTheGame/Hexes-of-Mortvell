using UnityEngine;
using System.Collections.Generic;
using HexCasters.Core.Grid;
using HexCasters.Core.Grid.Regions;

namespace HexCasters.Core.Units
{
	public class Movable : MonoBehaviour
	{
		public int movementPoints;

		public IEnumerable<BoardCell> FindReachableRegion(
			BoardCell currentCell)
		{
			return currentCell.Neighborhood(
				this.movementPoints,
				DestinationMovementCost);
		}

		private static int DestinationMovementCost(BoardCell from, BoardCell to)
		{
			return to.terrain.movementCost;
		}
	}
}