using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Grid.Regions;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.Core.Units
{
	[RequireComponent(typeof(BoardCellContent))]
	public class Unit : MonoBehaviour
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
