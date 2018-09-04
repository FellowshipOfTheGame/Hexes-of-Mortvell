using System;
using System.Linq;
using UnityEngine;
using HexCasters.DesignPatterns.Fsm;
using HexCasters.Core.Units.Teams;
using HexCasters.Core.Grid;
using HexCasters.Core.Units;
using HexCasters.GameModes.Common;
using HexCasters.Hud.Grid;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattleOverviewState : FsmState
	{
		public BattleTurn turn;
		public CellClickListener cellClickListener;
		public BattlePlayerOrders playerOrders;

		private IDisposable unmovedUnitsHighlight;

		public override void Enter()
		{
			Debug.Log(GetType());
			ApplyUnmovedUnitsHighlight();
			RegisterClickHandler();
		}

		public override void Exit()
		{
			RemoveUnmovedUnitsHighlight();
			UnregisterClickHandler();
		}

		void TrySelectUnit(BoardCell cell)
		{
			var content = cell.Content;
			var unit = content?.GetComponent<Unit>();
			if (unit == null)
				return;
			var teamMember = content?.GetComponent<TeamMember>();
			if (teamMember.team != this.turn.CurrentTeam)
				return;
			this.playerOrders.unit = unit;
			this.playerOrders.movementOrigin = cell;
			this.fsm.Transition<BattleSelectMovementDestinationState>();
		}

		void ApplyUnmovedUnitsHighlight()
		{
			var toBeHighlighted = this.turn.CurrentTeam.Members
				.Select(member => member.GetComponent<Unit>())
				.Where(unit => unit != null)
				.Where(unit => !unit.hasMoved)
				.Select(unit => unit.AsCellContent.Cell);
			this.unmovedUnitsHighlight =
				toBeHighlighted.AddHighlightLayer(Color.white);
		}

		void RemoveUnmovedUnitsHighlight()
		{
			this.unmovedUnitsHighlight.Dispose();
			this.unmovedUnitsHighlight = null;
		}

		void RegisterClickHandler()
		{
			this.cellClickListener.clickEvent += TrySelectUnit;
		}

		void UnregisterClickHandler()
		{
			this.cellClickListener.clickEvent -= TrySelectUnit;
		}
	}
}