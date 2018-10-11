using UnityEngine;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Loading;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.Testing.NeighborhoodBfs
{
	public class UnitTestBoardLoader : MonoBehaviour
	{
		public Board board;
		public BoardLayout layout;
		public GameObject unitPrefab;
		public GameObject highlightPrefab;
		private BoardCellContent unitInstanceCellContent;

		void Start()
		{
			board.LoadLayout(layout);
			this.unitInstanceCellContent = this.board.Spawn(
				unitPrefab,
				new BoardPosition(-2, 3));
			ShowMoveArea();
		}

		void ShowMoveArea()
		{
			var unitCell = this.unitInstanceCellContent.Cell;
			var unitUnit = this.unitInstanceCellContent
				.GetComponent<UnitTestUnit>();
			var area = unitCell.Neighborhood(
				unitUnit.movementPoints,
				(from, to) => to.Terrain.movementCost);
			foreach (var cell in area)
			{
				Instantiate(
					highlightPrefab,
					cell.Transform,
					false);
			}
		}
	}
}
