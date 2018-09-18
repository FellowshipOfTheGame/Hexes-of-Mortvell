using System;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.Testing.ActionsTest
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