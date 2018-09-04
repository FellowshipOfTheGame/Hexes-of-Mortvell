using System;
using System.Collections.Generic;
using UnityEngine;
using HexCasters.Core.Grid;
using HexCasters.Core.Grid.Regions;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestUnit : MonoBehaviour
	{
		public int movementPoints;
		public bool hasMoved;

		public IEnumerable<BoardCell> GetReachableCells(BoardCell cell)
		{
			return cell.Neighborhood(this.movementPoints, Distance);
		}

		int Distance(BoardCell from, BoardCell to)
		{
			if (!to.Empty)
				return this.movementPoints + 1; // can't enter tile
			return to.Terrain.movementCost;
		}
	}
}