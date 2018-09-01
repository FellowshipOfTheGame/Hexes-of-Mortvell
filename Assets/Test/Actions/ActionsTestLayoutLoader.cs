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
		public ActionsTestCellHoverListener cellHoverListener;

		void Start()
		{
			this.board.LoadLayout(layout);
			this.board.Spawn(playerPrefab, new BoardPosition(-1, -1));
		}
	}
}
