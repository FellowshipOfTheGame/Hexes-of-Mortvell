using UnityEngine;
using HexCasters.Core.Grid;
using HexCasters.Hud.Grid;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestLayoutLoader : MonoBehaviour
	{
		public Board board;
		public BoardLayout layout;
		public GameObject playerPrefab;
		public ActionsTestCellSelector cellSelector;

		void Start()
		{
			this.board.LoadLayout(layout);
			this.board.Spawn(playerPrefab, new BoardPosition(-1, -1));
			this.cellSelector.Initialize(this.board);
		}
	}
}
