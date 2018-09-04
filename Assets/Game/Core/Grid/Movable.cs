using System.Collections.Generic;
using UnityEngine;
using HexCasters.Core.Grid.Regions;

namespace HexCasters.Core.Grid
{
	[RequireComponent(typeof(BoardCellContent))]
	public class Movable : MonoBehaviour
	{
		public bool hasMoved;
		public int movementPoints;
		private BoardCellContent asCellContent;

		private int UnreachableCost => this.movementPoints + 1;

		void Awake()
		{
			this.asCellContent = GetComponent<BoardCellContent>();
		}

		public IEnumerable<BoardCell> ReachableCells()
		{
			return this.asCellContent.Cell
				.Neighborhood(
					this.movementPoints,
					distanceFunction: MovementCost);
		}

		int MovementCost(BoardCell from, BoardCell to)
		{
			if (to.Empty)
				return to.Terrain.movementCost;
			return this.UnreachableCost;
		}
	}
}
