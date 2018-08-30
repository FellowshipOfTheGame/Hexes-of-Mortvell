using UnityEngine;
using System.Collections.Generic;
using HexCasters.Core.Board;
using HexCasters.Core.Board.Regions;

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
				MovementCost);
		}

		private static int MovementCost(BoardCell from, BoardCell to)
		{
			return to.terrain.movementCost;
		}
	}
}