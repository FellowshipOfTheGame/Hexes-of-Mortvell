using System;
using System.Linq;
using UnityEngine;
using HexCasters.DesignPatterns.Fsm;
using HexCasters.Core.Units.Teams;
using HexCasters.Core.Grid;
using HexCasters.GameModes.Common;
using HexCasters.Hud.Grid;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattleOverviewState : FsmState
	{
		public BattleTurn turn;
		public CellClickListener cellClickListener;
		public BattlePlayerOrders playerOrders;

		private IDisposable unmovedMovablesHighlight;

		public override void Enter()
		{
			Debug.Log(GetType());
			ApplyUnmovedMovablesHighlight();
			RegisterClickListener();
		}

		public override void Exit()
		{
			RemoveUnmovedMovablesHighlight();
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
			this.playerOrders.movable = movable;
			this.playerOrders.movementOrigin = cell;
			this.fsm.Transition<BattleSelectMovementDestinationState>();
		}

		void ApplyUnmovedMovablesHighlight()
		{
			var toBeHighlighted = this.turn.CurrentTeam.Members
				.Select(member => member.GetComponent<Movable>())
				.Where(movable => movable != null)
				.Where(movable => !movable.hasMoved)
				.Select(movable => movable.AsCellContent.Cell);
			this.unmovedMovablesHighlight =
				toBeHighlighted.AddHighlightLayer(Color.white);
		}

		void RemoveUnmovedMovablesHighlight()
		{
			this.unmovedMovablesHighlight.Dispose();
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