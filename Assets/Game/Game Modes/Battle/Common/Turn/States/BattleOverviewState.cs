using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Units.Teams;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.GameModes.Common;
using HexesOfMortvell.Hud.Grid;

namespace HexesOfMortvell.GameModes.Battle.Common
{
	public class BattleOverviewState : FsmState
	{
		public BattleTurn turn;
		public CellClickListener cellClickListener;
		public BattlePlayerOrders playerOrders;
		public Button endTurn;

		private IDisposable unmovedUnitsHighlight;

		public override void Enter()
		{
			Debug.Log(GetType());
			ResetPlayerOrders();
			EnableEndTurnButton();
			ApplyUnmovedUnitsHighlight();
			RegisterClickHandler();
		}

		public override void Exit()
		{
			RemoveUnmovedUnitsHighlight();
			UnregisterClickHandler();
			DisableEndTurnButton();
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
				this.endTurn.onClick?.Invoke();
		}

		void ResetPlayerOrders()
		{
			this.playerOrders.Clear();
		}

		void TrySelectUnit(BoardCell cell)
		{
			var content = cell.Content;
			var unit = content?.GetComponent<Unit>();
			if (unit == null || unit.hasMoved)
				return;
			var teamMember = content?.GetComponent<TeamMember>();
			if (teamMember.team != this.turn.CurrentTeam)
				return;
			this.playerOrders.unit = unit;
			this.playerOrders.movementOrigin = cell;
			this.fsm.Transition<BattleSelectMovementDestinationState>();
		}

		void EnableEndTurnButton()
		{
			endTurn.interactable = true;
		}

		void DisableEndTurnButton()
		{
			endTurn.interactable = false;
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