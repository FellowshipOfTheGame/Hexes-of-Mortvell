using UnityEngine;
using HexCasters.Core.Grid;

namespace HexCasters.Testing.BoardTest
{
	public class BoardTestLayoutLoader : MonoBehaviour
	{
		public Board board;
		public BoardLayout testLayout;
		public GameObject testOrbPrefab;
		public GameObject testObstaclePrefab;
		public BoardCellContent testOrbInstance;

		void Start()
		{
			this.board.LoadLayout(testLayout);
			this.testOrbInstance = this.board.Spawn(
				testOrbPrefab, new BoardPosition(-2, -2));
			this.board.Spawn(testObstaclePrefab, new BoardPosition(0, 0));
		}

		void Update()
		{
			MoveOrbIfKeyPressed(KeyCode.E, Direction.PositiveY);
			MoveOrbIfKeyPressed(KeyCode.D, Direction.PositiveX);
			MoveOrbIfKeyPressed(KeyCode.X, Direction.ConstantZNegativeY);
			MoveOrbIfKeyPressed(KeyCode.Z, Direction.NegativeY);
			MoveOrbIfKeyPressed(KeyCode.A, Direction.NegativeX);
			MoveOrbIfKeyPressed(KeyCode.W, Direction.ConstantZPositiveY);
			PrintOrbCoordsIfKeyPressed(KeyCode.S);
		}

		void PrintOrbCoordsIfKeyPressed(KeyCode keyCode)
		{
			if (Input.GetKeyDown(keyCode))
				Debug.Log(testOrbInstance.Cell);
		}

		void MoveOrbIfKeyPressed(KeyCode keyCode, Direction direction)
		{
			if (Input.GetKeyDown(keyCode))
				MoveTestOrbInstance(direction);
		}

		void MoveTestOrbInstance(Direction direction)
		{
			var currentCell = this.testOrbInstance.Cell;
			var adjacentCell = currentCell.FindAdjacentCell(direction);
			if (adjacentCell != null && adjacentCell.Empty)
				currentCell.MoveContentTo(adjacentCell);
		}
	}

}