using UnityEngine;
using HexCasters.Core.Grid;
using HexCasters.Core.Units.Teams;
using HexCasters.Hud.Grid;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestLayoutLoader : MonoBehaviour
	{
		public Board board;
		public BoardLayout layout;
		public GameObject playerPrefab;
		public ActionsTestCellHoverListener cellHoverListener;

		public Team team1;
		public Team team2;

		void Start()
		{
			this.board.LoadLayout(layout);
			var p1 = this.board.Spawn(playerPrefab, new BoardPosition(-1, -1));
			var p2 = this.board.Spawn(playerPrefab, new BoardPosition(1, 1));

			team1.Add(p1.gameObject);
			team2.Add(p2.gameObject);
		}
	}
}
