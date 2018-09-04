using UnityEngine;
using System.Linq;
using HexCasters.DesignPatterns.Fsm;
using HexCasters.Core.Units.Teams;
using HexCasters.Core.Grid;
using HexCasters.GameModes.Common;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattleOverviewState : FsmState
	{
		public BattleTurn turn;
		public CellClickListener cellClickListener;
		public BattlePlayerOrders playerOrders;

		public override void Enter()
		{
			Debug.Log(GetType());
			HighlightUnmovedMovables();
			RegisterClickListener();
		}

		public override void Exit()
		{
			UnregisterClickListener();
		}

		void TrySelectUnit(BoardCell cell)
		{
			var content = cell.Content;
			var movable = content?.GetComponent<Movable>();
			if (movable == null)
				return;
			var teamMember = content?.GetComponent<TeamMember>();
			if (teamMember.team != this.turn.CurrentTeam)
				return;
			this.playerOrders.movementOrigin = cell;
			this.fsm.Transition<BattleSelectMovementDestinationState>();
		}

		void HighlightUnmovedMovables()
		{
			var currentTeamMovables = this.turn.CurrentTeam.Members
				.Select(member => member.GetComponent<Movable>())
				.Where(movable => movable != null)
				.Where(movable => !movable.hasMoved);
			foreach (var movable in currentTeamMovables)
				ApplyUnmovedHighlight(movable.AsCellContent.Cell);
		}

		void ApplyUnmovedHighlight(BoardCell cell)
		{
			Debug.LogFormat("{0} has an unmoved movable", cell);
			// TODO
		}

		void RegisterClickListener()
		{
			this.cellClickListener.clickEvent += TrySelectUnit;
		}

		void UnregisterClickListener()
		{
			this.cellClickListener.clickEvent -= TrySelectUnit;
		}
	}
}