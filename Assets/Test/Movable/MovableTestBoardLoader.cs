using UnityEngine;
using HexCasters.Core.Units;
using HexCasters.Core.Grid;
using HexCasters.Core.Grid.Regions;

public class MovableTestBoardLoader : MonoBehaviour
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
		var unitMovable = this.unitInstanceCellContent.GetComponent<Movable>();
		var area = unitCell.Neighborhood(
			unitMovable.movementPoints,
			(from, to) => to.Terrain.movementCost);
		foreach (var cell in area)
		{
			Instantiate(
				highlightPrefab,
				cell.transform,
				false);
		}
	}
}
