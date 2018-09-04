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
		public BoardCellContent AsCellContent
		{
			get;
			private set;
		}

		private int UnreachableCost => this.movementPoints + 1;

		void Awake()
		{
			this.AsCellContent = GetComponent<BoardCellContent>();
		}

		public IEnumerable<BoardCell> ReachableCells()
		{
			return this.AsCellContent.Cell
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
