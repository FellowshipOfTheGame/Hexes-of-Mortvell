using System;
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

		public event Action DoneLoadingEvent;

		public Team team1;
		public Team team2;

		void Start()
		{
			this.board.LoadLayout(layout);
			var t1p1 = this.board.Spawn(playerPrefab, new BoardPosition(0, -2));
			var t1p2 = this.board.Spawn(playerPrefab, new BoardPosition(-2, 0));
			var t2p1 = this.board.Spawn(playerPrefab, new BoardPosition(0, 2));
			var t2p2 = this.board.Spawn(playerPrefab, new BoardPosition(2, 0));

			team1.Add(t1p1.gameObject);
			team1.Add(t1p2.gameObject);
			team2.Add(t2p1.gameObject);
			team2.Add(t2p2.gameObject);

			DoneLoadingEvent?.Invoke();
		}
	}
}
