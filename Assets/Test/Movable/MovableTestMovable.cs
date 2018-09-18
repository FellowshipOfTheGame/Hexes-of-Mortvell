using UnityEngine;
using System.Collections.Generic;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.Testing.NeighborhoodBfs
{
	[RequireComponent(typeof(BoardCellContent))]
	public class UnitTestUnit : MonoBehaviour
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

		static int DestinationMovementCostAsDistance(
			BoardCell from,
			BoardCell to)
		{
			return to.Terrain.movementCost;
		}
	}
}
